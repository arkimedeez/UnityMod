﻿using arkimedeezMod.DamageClasses;
using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Steamworks;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static tModPorter.ProgressUpdate;

namespace arkimedeezMod.Items.Weapons.SkySplitter
{
    // ExampleCustomSwingSword is an example of a sword with a custom swing using a held projectile
    // This is great if you want to make melee weapons with complex swing behavior
    // Note that this projectile only covers 2 relatively simple swings, everything else is up to you
    // Aside from the custom animation, the custom collision code in Colliding is very important to this weapon
    public class SkySplitterProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 186; // The width of projectile hitbox
            Projectile.height = 186; // The height of projectile hitbox
            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.hostile = false; // Can the projectile deal damage to the player?
            Projectile.DamageType = ModContent.GetInstance<OmegaDamage>();
            Projectile.timeLeft = 6000;
            Projectile.penetrate = -1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.light = 0.5f; // How much light emit around the projectile
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false; // Can the projectile collide with tiles?
            Projectile.extraUpdates = 0; // Set to above 0 if you want the projectile to update multiple time in a frame
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            UnityPlayer.OmegaChargeCurrent += 0.75f;
        }

        private Player Owner => Main.player[Projectile.owner];

        float r = 0.25f;

        float SoundCooldown = 0;

        SoundStyle Swish = new SoundStyle($"{nameof(arkimedeezMod)}/Assets/Audio/SpinningSword");

        public override void AI()
        {
            // Kill the projectile if the player dies or gets crowd controlled
            Owner.heldProj = Projectile.whoAmI;
            Owner.SetDummyItemTime(2);
            if (!Owner.active || Owner.dead || Owner.noItems || Owner.CCed || Owner.HeldItem.ModItem is not SkySplitter)
            {
                Projectile.Kill();
                return;
            }
            if (Owner.channel)
            {
                Projectile.Center = Owner.Center;
                Projectile.rotation += r;
                r += 0.005f;
                if (r > 0.3f)
                {
                    r = 0.3f;
                }
            }
            else
            {
                Projectile.Kill();
            }

            if (SoundCooldown < 2)
            {
                SoundCooldown = SoundCooldown + 0.1f;
            }
            else
            {
                SoundEngine.PlaySound(Swish with { Volume = 0.2f } with { PitchVariance = 0.2f });
                SoundCooldown = 0;
            }
            /*if(OmegaCharge.OmegaChargeCurrent > 99)
            {
                if(TextShown == false)
                {
                    CombatText.NewText(Owner.Hitbox, new Color(75, 0, 130), "Omega Charged!", true);
                    SoundEngine.PlaySound(SoundID.Item6);
                    TextShown = true;
                }
            }
            */
        }
    }
}