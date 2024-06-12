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
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2; // The recording mode
        }
        public override string Texture => "arkimedeezMod/Items/Weapons/Venetration/Venetration";

        public override void SetDefaults()
        {
            AIType = ProjectileID.WoodenBoomerang;
            Projectile.CloneDefaults(ProjectileID.WoodenBoomerang);
            Projectile.DamageType = ModContent.GetInstance<OmegaDamage>();
            Projectile.width = 204;
            Projectile.height = 204;
            Projectile.tileCollide = false;
            Projectile.penetrate = 400;
            //Projectile.friendly = false;
            //Projectile.hostile = false;
           // Projectile.extraUpdates = 0;
        }

        public override void AI()
        {
            Projectile.rotation += 0.15f;
            Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.InfernoFork, Projectile.velocity.X * 0f, Projectile.velocity.Y * 0f, Alpha: 128, Scale: 1.2f);
            
        }
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.damage = Main.LocalPlayer.HeldItem.damage;
            base.OnSpawn(source);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            //UnityPlayer.OmegaChargeCurrent = UnityPlayer.OmegaChargeCurrent + 1.5f;
            target.AddBuff(BuffID.OnFire, 400);
            Projectile.damage = Convert.ToInt32(Math.Round(Projectile.damage * 0.8));
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
                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale * 1.2f, SpriteEffects.None, 0);
            }

            return false;
        }
    }
}