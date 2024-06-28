using arkimedeezMod.DamageClasses;
using arkimedeezMod.Items.Materials;
using Microsoft.Build.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace arkimedeezMod.Items.Accessories.BloodlustDagger
{
    public class BloodlustDagger : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 54;
            Item.accessory = true;
            Item.value = Item.sellPrice(gold: 1, silver: 50);
            Item.rare = ItemRarityID.Orange;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<PlayerEvents>().BloodlustDagger = true;
        }

        /*public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<HeavengoldBar>(8)
            .AddIngredient(ItemID.CopperBar, 10)
            .AddTile(TileID.Anvils)
            .Register();

            CreateRecipe()
             .AddIngredient<HeavengoldBar>(8)
            .AddIngredient(ItemID.TinBar, 10)
            .AddTile(TileID.Anvils)
            .Register();
        }*/
    }
}
