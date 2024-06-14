using Microsoft.Xna.Framework;
using Terraria;
using arkimedeezMod.Items.Materials;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using arkimedeezMod.Projectiles;
using arkimedeezMod.DamageClasses;
using Terraria.Audio;
using System;
using SteelSeries.GameSense;

namespace arkimedeezMod.Items.Weapons.Venetration
{
    // ExampleCustomSwingSword is an example of a sword with a custom swing using a held projectile
    // This is great if you want to make melee weapons with complex swing behavior
    public class Venetration : ModItem
    {
        public override void SetDefaults()
        {
            // Common Properties
            Item.width = 204;
            Item.height = 204;
            Item.value = Item.sellPrice(gold: 2, silver: 50);
            Item.rare = ItemRarityID.Orange;

            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.crit = 9;
            Item.autoReuse = true;

            // Weapon Properties
            Item.knockBack = 7;  // The knockback of your sword, this is dynamically adjusted in the projectile code.
            Item.autoReuse = false; // This determines whether the weapon has autoswing
            Item.damage = 20; // The damage of your sword, this is dynamically adjusted in the projectile code.
            Item.DamageType = ModContent.GetInstance<OmegaDamage>();
            Item.noUseGraphic = true; // This makes sure the item does not get shown when the player swings his hand
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.UseSound = new SoundStyle($"{nameof(arkimedeezMod)}/Assets/Audio/SwordSlash7")
            {
                Volume = 0.3f,
                PitchVariance = 0.3f,
            };

            Item.shoot = ModContent.ProjectileType<VenetrationProjectile>();
        }

        public override bool AltFunctionUse(Player player) => true;

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                Item.useTime = 36;
                Item.useAnimation = 36;
                Item.crit = 9;
                Item.autoReuse = true;
                Item.shootSpeed = 0;

                // Weapon Properties
                Item.knockBack = 7;  // The knockback of your sword, this is dynamically adjusted in the projectile code.
                Item.autoReuse = false; // This determines whether the weapon has autoswing
                //Item.damage = 20; // The damage of your sword, this is dynamically adjusted in the projectile code.
                Item.DamageType = ModContent.GetInstance<OmegaDamage>();
                Item.noUseGraphic = true; // This makes sure the item does not get shown when the player swings his hand
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.noMelee = true;
                Item.UseSound = new SoundStyle($"{nameof(arkimedeezMod)}/Assets/Audio/SwordSlash7")
                {
                    Volume = 0.3f,
                    PitchVariance = 0.3f,
                };

                Item.shoot = ModContent.ProjectileType<VenetrationProjectile>(); // The sword as a projectile
                return base.CanUseItem(player);
            }
            else
            {
                if (UnityPlayer.OmegaChargeCurrent == 100)
                {
                    UnityPlayer.OmegaChargeCurrent = 0;
                    Item.shootSpeed = 5;
                    Item.useTime = 200;
                    Item.useAnimation = 200;
                    //Item.damage = 300;
                    Item.UseSound = new SoundStyle($"{nameof(arkimedeezMod)}/Assets/Audio/SwordSlash2");
                    Item.shoot = ModContent.ProjectileType<VenetrationThrownProjectile>();
                    player.AddBuff(BuffID.BrokenArmor, 300);
                }
                else
                {
                    Item.shoot = ProjectileID.None;
                    Item.UseSound = SoundID.Item1;
                }
                return base.CanUseItem(player);
            }
        }

        /*
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(3))
            {
                // Emit dusts when the sword is swung
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.InfernoFork);
            }
        }
        */
    }
}