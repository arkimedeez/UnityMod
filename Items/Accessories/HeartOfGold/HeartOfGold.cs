using arkimedeezMod.Buffs.StatBoosts;
using arkimedeezMod.DamageClasses;
using arkimedeezMod.Items.Materials;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace arkimedeezMod.Items.Accessories.HeartOfGold
{
    public class HeartOfGold : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 32;
            Item.accessory = true;
            Item.value = Item.sellPrice(gold: 1, silver: 50);
            Item.rare = ItemRarityID.Orange;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) += 15 / 100f;
                player.lifeRegen += -3;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<HeavengoldBar>(4)
            .AddIngredient(ItemID.LifeCrystal, 1)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
