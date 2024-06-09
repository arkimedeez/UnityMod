﻿using arkimedeezMod.DamageClasses;
using arkimedeezMod.Items.Weapons.StarSeeker;
using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace arkimedeezMod.Projectiles
{
    public class StarSeekerProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 42; // The width of projectile hitbox
            Projectile.height = 42; // The height of projectile hitbox
            Projectile.aiStyle = 1; // The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.hostile = false; // Can the projectile deal damage to the player?
            Projectile.DamageType = ModContent.GetInstance<OmegaDamage>();
            Projectile.penetrate = 1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 90; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 255; // The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0.5f; // How much light emit around the projectile
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true; // Can the projectile collide with tiles?
            Projectile.extraUpdates = 0; // Set to above 0 if you want the projectile to update multiple time in a frame

            AIType = ProjectileID.Bullet; // Act exactly like default Bullet
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (StarSeeker.ShootType == 0)
            {
                UnityPlayer.OmegaChargeCurrent = UnityPlayer.OmegaChargeCurrent + 1.5f;
            } 
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

            return true;
        }

        public override void OnKill(int timeLeft)
        {
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }

        float r = 0;

        public override void AI()
        {
            int random = Main.rand.Next(1, 5);
            if(random == 4)
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PinkCrystalShard, Projectile.velocity.X * 0f, Projectile.velocity.Y * 0f, Alpha: 128, Scale: 1.2f);
            }
            if (!Main.dedServ)
            {
                Projectile.velocity *= 0.99f;
                Projectile.rotation += r;
                r += 0.3f;
                Projectile.scale *= 0.99f;
            }
        }
    }
}