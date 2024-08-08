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
using arkimedeezMod.Buffs;
using arkimedeezMod.Items.Weapons.StarSeeker;
using arkimedeezMod.Items.Weapons.SangriaEvaporator;

namespace arkimedeezMod.Items.Weapons.Venetration
{
    // ExampleCustomSwingSword is an example of a sword with a custom swing using a held projectile
    // This is great if you want to make melee weapons with complex swing behavior
    public class Venetration : ModItem
    {
        public override string Texture => "arkimedeezMod/Items/Weapons/Venetration/Venetration";

        public override void SetDefaults()
        {
            // Common Properties
            Item.width = 80;
            Item.height = 80;
            Item.value = Item.sellPrice(gold: 2, silver: 50);
            Item.rare = ItemRarityID.Orange;

            Item.useTime = 40;
            Item.useAnimation = 40;

            Item.crit = 9;
            Item.autoReuse = true;

            // Weapon Properties
            Item.knockBack = 7;  // The knockback of your sword, this is dynamically adjusted in the projectile code.
            Item.autoReuse = false; // This determines whether the weapon has autoswing
            Item.damage = 45; // The damage of your sword, this is dynamically adjusted in the projectile code.
            Item.DamageType = ModContent.GetInstance<OmegaDamage>();
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = new SoundStyle($"{nameof(arkimedeezMod)}/Assets/Audio/SwordSlash7")
            {
                Volume = 0.3f,
                PitchVariance = 0.3f,
            };

            //Item.shoot = ModContent.ProjectileType<VenetrationProjectile>();
            Item.shoot = ModContent.ProjectileType<VenetrationSwingProjectile>();
        }

        public override bool AltFunctionUse(Player player) => true;

        public int ShootType;

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                ShootType = 0;
                Item.useTime = 36;
                Item.useAnimation = 36;
                Item.crit = 9;
                Item.autoReuse = true;
                Item.shootSpeed = 0;
                Item.noUseGraphic = false;

                // Weapon Properties
                Item.knockBack = 7;  // The knockback of your sword, this is dynamically adjusted in the projectile code.
                Item.autoReuse = false; // This determines whether the weapon has autoswing
                Item.DamageType = ModContent.GetInstance<OmegaDamage>();
                Item.useStyle = ItemUseStyleID.Swing;
                Item.UseSound = new SoundStyle($"{nameof(arkimedeezMod)}/Assets/Audio/SwordSlash7")
                {
                    Volume = 0.3f,
                    PitchVariance = 0.3f,
                };

                Item.shoot = ModContent.ProjectileType<VenetrationSwingProjectile>();
                return base.CanUseItem(player);
            }
            else
            {
                if (UnityPlayer.OmegaChargeCurrent == 100)
                {
                    ShootType = 1;
                    UnityPlayer.OmegaChargeCurrent = 0;
                    Item.shootSpeed = 5;
                    Item.useTime = 200;
                    Item.useAnimation = 200;
                    Item.noUseGraphic = true;
                    Item.UseSound = new SoundStyle($"{nameof(arkimedeezMod)}/Assets/Audio/SwordSlash2");
                    player.AddBuff(ModContent.BuffType<ShatteredArmor>(), 400);
                }
                /*else
                {
                    Item.shoot = ProjectileID.None;
                    Item.UseSound = SoundID.Item1;
                }*/
                return base.CanUseItem(player);
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (ShootType == 0)
            {
                float adjustedItemScale = player.GetAdjustedItemScale(Item);// Get the melee scale of the player and item.
                Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
                NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI); // Sync the changes in multiplayer.
            }
            else
            {
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<VenetrationThrownProjectile>(), damage * 12, knockback);
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<ClothiersDelight>(10)
            .AddIngredient(ItemID.FieryGreatsword)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}