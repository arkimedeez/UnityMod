using arkimedeezMod.DamageClasses;
using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace arkimedeezMod.Items.Weapons.JoiseMegaphone
{
    public class MegaphoneProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 20; // The width of projectile hitbox
            Projectile.height = 20; // The height of projectile hitbox
            Projectile.aiStyle = 1; // The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.hostile = false; // Can the projectile deal damage to the player?
            Projectile.DamageType = ModContent.GetInstance<OmegaDamage>();
            Projectile.penetrate = 10; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 600; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 255; // The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0.5f; // How much light emit around the projectile
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true; // Can the projectile collide with tiles?
            Projectile.extraUpdates = 1; // Set to above 0 if you want the projectile to update multiple time in a frame
            AIType = ProjectileID.Bullet; // Act exactly like default Bullet
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            UnityPlayer.OmegaChargeCurrent = UnityPlayer.OmegaChargeCurrent + 3.5f;
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
            // Avoid spawning dusts on dedicated servers
            if (!Main.dedServ)
            {
                int random = Main.rand.Next(1, 4);
                if (random == 2)
                {
                    Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Cloud, Projectile.velocity.X * 0f, Projectile.velocity.Y * 0f, Alpha: 128, Scale: 1.2f);
                }
            }
            return true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.penetrate--;
            Projectile.damage += 5;
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon) Projectile.velocity.X = -oldVelocity.X;
            if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon) Projectile.velocity.Y = -oldVelocity.Y;
            return false;
        }

        float maxScale = 2;

        public override void AI()
        {
            Projectile.velocity *= 1.01f;
            if (Projectile.scale <= maxScale)
            {
                Projectile.scale *= 1.01f;
            }
        }
    }
}