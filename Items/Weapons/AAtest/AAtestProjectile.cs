using arkimedeezMod.DamageClasses;
using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace arkimedeezMod.Items.Weapons.AAtest
{
    public class AAtestProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2; // The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.width = 102;
            Projectile.height = 92;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.extraUpdates = 2;
        }

        public bool aimingVertically;
        public int direction;
        public float frameCounter = 0f;

        public override void AI()
        {
            Projectile.frameCounter += 1;
            Player player = Main.player[Projectile.owner];
            var modPlayer = player.GetModPlayer<ItemDirectionPlayer>();

            if (frameCounter == 0)
            {
                aimingVertically = Math.Abs(Projectile.Center.X - Main.MouseWorld.X) < Math.Abs(Projectile.Center.Y - Main.MouseWorld.Y);
                if (aimingVertically)
                {
                    direction = Projectile.Center.Y > Main.MouseWorld.Y ? -1 : 1;
                }
                else
                {
                    direction = Projectile.Center.X < Main.MouseWorld.X ? -1 : 1;
                }
                modPlayer.swordDirection *= -1;
            }

            frameCounter += (float)(1 - Math.Cos((float)Projectile.frameCounter / 8f)) / 12;
            if (aimingVertically)
            {
                float angle = (float)(90 * direction + (Math.Cos(frameCounter) * 120 * modPlayer.swordDirection));
                Projectile.rotation = (float)(angle * (Math.PI / 180));
            }
            else
            {
                float angle = (float)(90 + 90 * direction + (Math.Cos(frameCounter) * 120 * modPlayer.swordDirection));
                Projectile.rotation = (float)(angle * (Math.PI / 180));
            }

            Projectile.Center = player.Center + (Projectile.rotation.ToRotationVector2() * Projectile.width / 2);

            if (!aimingVertically)
            {
                player.ChangeDir(-direction);
            }

            player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (float)(Projectile.rotation - Math.PI / 2));
            if (frameCounter > Math.PI)
            {
                Projectile.Kill();
            }

            if (Projectile.frameCounter == 18)
            {
                Projectile.friendly = true;
            }
            else if (Projectile.frameCounter == 36)
            {
                Projectile.friendly = false;
            }

            Projectile.spriteDirection = direction;

            if (direction == -1)
            {
                Projectile.rotation += (float)(Math.PI);
            }

            if (Projectile.spriteDirection == -1)
            {
                // If sprite is facing left, rotate
                if (modPlayer.swordDirection > 0)
                {
                    Projectile.rotation += MathHelper.ToRadians(45f);
                }
                else
                {
                    Projectile.rotation += MathHelper.ToRadians(135f);
                }
            }
            else
            {
                // If sprite is facing right, rotate
                if (modPlayer.swordDirection > 0)
                {
                    Projectile.rotation += MathHelper.ToRadians(225f);
                }
                else
                {
                    Projectile.rotation += MathHelper.ToRadians(315);
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects;
            Player player = Main.player[Projectile.owner];
            var modPlayer = player.GetModPlayer<ItemDirectionPlayer>();

            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);

            if (modPlayer.swordDirection < 0)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                effects = SpriteEffects.None;
            }

            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
            }

            return false;
        }
    }
}