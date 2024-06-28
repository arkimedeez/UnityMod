using arkimedeezMod.Buffs.StatBoosts;
using arkimedeezMod.DamageClasses;
using arkimedeezMod.Items.Materials;
using arkimedeezMod.Items.Weapons.SangioveseBoiler;
using arkimedeezMod.Items.Weapons.WrathOfTheHarpies;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace arkimedeezMod.Items.Weapons.SangriaEvaporator
{
    public class SangriaEvaporator : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Type] = true;
        }

        public override void SetDefaults()
        {
            // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

            // Common Properties
            Item.width = 62; // Hitbox width of the item.
            Item.height = 38; // Hitbox height of the item.
            Item.scale = 0.75f;
            Item.value = Item.sellPrice(gold: 1, silver: 50);
            Item.rare = ItemRarityID.Orange;

            //Common stats 
            Item.damage = 20;
            Item.crit = 3;
            Item.knockBack = 1.5f;
            Item.DamageType = ModContent.GetInstance<OmegaDamage>();

            // Use Properties
            Item.useTime = 15; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 15; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.UseSound = SoundID.Item12;

            //Shoot stats
            Item.shoot = ModContent.ProjectileType<SangriaEvaporatorProjectile>();
            Item.shootSpeed = 10f;

            //Misc stats
            Item.noMelee = true;
            Item.autoReuse = true;
        }

        public override bool AltFunctionUse(Player player) => true;

        public static int ShootType = 0;

        public override bool CanUseItem(Player player) 

        {
            if (player.altFunctionUse != 2)
            {
                ShootType = 0;

                // Use Properties
                Item.useTime = 15; // The item's use time in ticks (60 ticks == 1 second.)
                Item.useAnimation = 15; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            }
            else
            {
                if (UnityPlayer.OmegaChargeCurrent == 100)
                {
                    Item.useTime = 1;
                    Item.useAnimation = 30;
                    ShootType = 1;
                    UnityPlayer.OmegaChargeCurrent = 0;
                    SoundEngine.PlaySound(SoundID.Item20 with { Volume = 100f });
                    player.AddBuff(ModContent.BuffType<DefenseII>(), 1200);
                }
            }
                return base.CanUseItem(player);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if(ShootType == 0)
            {
                velocity = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(10));
            }
            else
            {
                velocity = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(30));
            }
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;

            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if(ShootType == 0)
            {
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SangriaEvaporatorProjectile>(), damage, knockback);
            }
            else
            {
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SangriaEvaporatorProjectile>(), damage * 7, knockback);
            }
            return false;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

        // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
        public override Vector2? HoldoutOffset() => new Vector2(0f, 0f);

        /*public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<HeavengoldBar>(8)
            .AddIngredient(ItemID.Feather, 5)
            .AddTile(TileID.Anvils)
            .Register();
        }*/
    }
}