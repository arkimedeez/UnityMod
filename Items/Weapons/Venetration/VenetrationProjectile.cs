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

namespace arkimedeezMod.Items.Weapons.Venetration
{
    public class VenetrationProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2; // The recording mode
        }

        public override string Texture => "arkimedeezMod/Items/Weapons/Venetration/Venetration";

        public override void SetDefaults()
        {
            Projectile.DamageType = ModContent.GetInstance<OmegaDamage>();
            Projectile.width = 204;
            Projectile.height = 204;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.extraUpdates = 0;
            Projectile.damage = 20;
            //Projectile.stopsDealingDamageAfterPenetrateHits = true;
        }

        public bool aimingVertically;
        public int direction;
        public float frameCounter = 0f;

        public override void AI()
        {
            Projectile.velocity = Projectile.velocity.RotatedBy(0.01f);
            Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.InfernoFork, Projectile.velocity.X * 0f, Projectile.velocity.Y * 0f, Alpha: 128, Scale: 1.2f);

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

            if (Projectile.frameCounter == 20)
            {
                Projectile.friendly = true;
            }
            else if (Projectile.frameCounter == 40)
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
                    Projectile.rotation += MathHelper.ToRadians(-90);
            }
            else
            {
                // If sprite is facing right, rotate
                    Projectile.rotation += MathHelper.ToRadians(90);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            UnityPlayer.OmegaChargeCurrent += 1.5f;
            target.AddBuff(BuffID.OnFire, 400);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            SpriteBatch spriteBatch = Main.spriteBatch;
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Type]; i++)
            {
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
                spriteBatch.Draw(texture, Projectile.oldPos[i] - Main.screenPosition + (Projectile.Center - Projectile.position), null, color * (1 - (float)i / 20), Projectile.oldRot[i], new Vector2(Projectile.width / 2, Projectile.height / 2), Projectile.scale, Projectile.oldSpriteDirection[i] == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            }
            return true;
        }
    }
}