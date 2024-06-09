using Microsoft.Xna.Framework;
using Terraria;
using arkimedeezMod.Items.Materials;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using arkimedeezMod.Projectiles;
using arkimedeezMod.DamageClasses;

namespace arkimedeezMod.Items.Weapons.AAtest
{
    // ExampleCustomSwingSword is an example of a sword with a custom swing using a held projectile
    // This is great if you want to make melee weapons with complex swing behavior
    public class AAtest : ModItem
    {
        public override void SetDefaults()
        {
            // Common Properties
            Item.width = 140;
            Item.height = 40;
            Item.value = Item.sellPrice(gold: 2, silver: 50);
            Item.rare = ItemRarityID.Orange;

            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.crit = 9;

            // Weapon Properties
            Item.knockBack = 7;  // The knockback of your sword, this is dynamically adjusted in the projectile code.
            Item.autoReuse = false; // This determines whether the weapon has autoswing
            Item.damage = 62; // The damage of your sword, this is dynamically adjusted in the projectile code.
            Item.DamageType = ModContent.GetInstance<OmegaDamage>();
            Item.noUseGraphic = true; // This makes sure the item does not get shown when the player swings his hand
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;

            Item.shoot = ModContent.ProjectileType<AAtestProjectile>();
        }
    }
}