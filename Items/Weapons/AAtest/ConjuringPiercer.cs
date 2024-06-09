/*using arkimedeezMod.Buffs;
using arkimedeezMod.DamageClasses;
using arkimedeezMod.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace arkimedeezMod.Items.Weapons.ConjuringPiercer
{
    public class ConjuringPiercer : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

        public override void SetDefaults()
        {
            // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

            // Common Properties
            Item.width = 62; // Hitbox width of the item.
            Item.height = 32; // Hitbox height of the item.
            Item.scale = 1f;
            Item.rare = ItemRarityID.Orange; // The color that the item's name will be in-game.

            // Use Properties
            Item.useTime = 24; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 24; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
            Item.consumeAmmoOnLastShotOnly = true;

            // Weapon Properties
            Item.DamageType = ModContent.GetInstance<OmegaDamage>();
            Item.damage = 65; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.knockBack = 5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.noMelee = true; // So the item's animation doesn't do damage.
            Item.crit = 7;

            // Gun Properties
            Item.shoot = ModContent.ProjectileType<ConjuringPiercerProjectile>();
            Item.UseSound = SoundID.Item36;
            Item.shootSpeed = 150f;
        }

        // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-15f, 2f);
        }

        // How can I make the shots appear out of the muzzle exactly?
        // Also, when I do this, how do I prevent shooting through tiles?
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;

            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            if (type == ProjectileID.Bullet)
            {
                type = ModContent.ProjectileType<ConjuringPiercerProjectile>();
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<Materials.HeavengoldBar>(6)
            .AddIngredient(ItemID.MeteoriteBar, 6)
            .AddIngredient(ItemID.Handgun, 1)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}*/