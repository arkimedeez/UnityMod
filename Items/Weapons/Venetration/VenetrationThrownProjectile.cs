using arkimedeezMod.DamageClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace arkimedeezMod.Items.Weapons.Venetration
{
    public class VenetrationThrownProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2; // The recording mode
        }

        //public override string Texture => "arkimedeezMod/Items/Weapons/Venetration/Venetration";

        public override void SetDefaults()
        {
            Projectile.DamageType = ModContent.GetInstance<OmegaDamage>();
            Projectile.width = 184;
            Projectile.height = 184;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.extraUpdates = 1;
        }

        private Player Owner => Main.player[Projectile.owner];

        private float timeSinceThrown = -60f;

        public override void AI()
        {
            timeSinceThrown++;
            Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.InfernoFork, Projectile.velocity.X * 0f, Projectile.velocity.Y * 0f, Alpha: 128, Scale: 1.2f);
            Projectile.rotation += 0.25f;
            if (timeSinceThrown > 0)
            {
                if (timeSinceThrown < 120)
                {
                    Projectile.velocity *= 0.98f;
                }
                else
                {
                    Projectile.velocity = Projectile.velocity.Length() * (Owner.MountedCenter - Projectile.Center).SafeNormalize(Vector2.One) * 1.013f;
                    if ((Projectile.Center - Owner.MountedCenter).Length() < 24f)
                    {
                        Projectile.Kill();
                    }
                }
            }
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.damage = (Main.LocalPlayer.HeldItem.damage) * 15;
            base.OnSpawn(source);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            //UnityPlayer.OmegaChargeCurrent = UnityPlayer.OmegaChargeCurrent + 1.5f;
            target.AddBuff(BuffID.OnFire, 400);
            Projectile.damage = Convert.ToInt32(Math.Round(Projectile.damage * 0.75));
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

            // Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }

            return false;
        }
    }
}