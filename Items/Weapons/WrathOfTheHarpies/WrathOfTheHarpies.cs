using arkimedeezMod.DamageClasses;
using arkimedeezMod.Items.Materials;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace arkimedeezMod.Items.Weapons.WrathOfTheHarpies
{
    public class WrathOfTheHarpies : ModItem
    {
        public override void SetDefaults()
        {
            // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

            // Common Properties
            Item.width = 62; // Hitbox width of the item.
            Item.height = 32; // Hitbox height of the item.
            Item.scale = 1f;
            Item.value = Item.sellPrice(gold: 1, silver: 50);
            Item.rare = ItemRarityID.LightRed;

            //Common stats 
            Item.damage = 40;
            Item.crit = 2;
            Item.knockBack = 1f;
            Item.DamageType = ModContent.GetInstance<OmegaDamage>();

            // Use Properties
            Item.useTime = 3; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 18; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)  
            Item.UseSound = SoundID.Item5; // The sound when the weapon is being used.
            Item.reuseDelay = 32;

            //Shoot stats
            Item.shoot = ModContent.ProjectileType<FeatherProjectile>();
            Item.shootSpeed = 30f;

            //Misc stats
            Item.noMelee = true;
            Item.autoReuse = true;  // Whether or not you can hold click to automatically use it again.
            Item.consumeAmmoOnLastShotOnly = true;
        }

       // int abilityMode = 0;

        public override bool AltFunctionUse(Player player) => true;

        public int ShootType = 0;

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                ShootType = 0;

                Item.useTime = 3; // The item's use time in ticks (60 ticks == 1 second.)
                Item.useAnimation = 18; // The length of the item's use animation in ticks (60 ticks == 1 second.)
                Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
                Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
                Item.UseSound = SoundID.Item5; // The sound when the weapon is being used.
                Item.reuseDelay = 32;
            }
            else
            {
                if (UnityPlayer.OmegaChargeCurrent == 100)
                {
                    ShootType = 1;

                    Item.useTime = 4; // The item's use time in ticks (60 ticks == 1 second.)
                    Item.useAnimation = 92; // The length of the item's use animation in ticks (60 ticks == 1 second.)
                    Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
                    Item.autoReuse = false; // Whether or not you can hold click to automatically use it again
                
                    UnityPlayer.OmegaChargeCurrent = 0;
                    Item.UseSound = new SoundStyle($"{nameof(arkimedeezMod)}/Assets/Audio/Gunshot3")
                    {
                        Volume = 1f,
                        PitchVariance = 0f,
                        MaxInstances = 3,
                    };
                }
            }
                return base.CanUseItem(player);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            int deviation = ShootType == 0 ? 5 : 50;
            velocity = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(deviation));
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;

            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (ShootType == 0)
            {
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<FeatherProjectile>(), damage, knockback, Main.myPlayer);
                return false; // return false to prevent original projectile from being shot
            }
            else
            {
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<FeatherProjectileAlt1>(), Convert.ToInt32(Math.Round(damage * 2.5)), knockback, Main.myPlayer);
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<FeatherProjectileAlt2>(), Convert.ToInt32(Math.Round(damage * 2.5)), knockback, Main.myPlayer);
                return false;
            }
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

        // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
        public override Vector2? HoldoutOffset() => new Vector2(-6f, -2f);
        

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<HeavengoldBar>(8)
            .AddIngredient(ItemID.SoulofFlight, 6)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}