using arkimedeezMod.DamageClasses;
using arkimedeezMod.Items.Materials;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace arkimedeezMod.Items.Accessories.DemonicVengance
{
    public class DemonicVengance : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 40;
            Item.accessory = true;
            Item.value = Item.sellPrice(gold: 1, silver: 50);
            Item.rare = ItemRarityID.Orange;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<PlayerEvents>().DemonicVengance = true;
        }

        /*public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<HeavengoldBar>(6)
            .AddIngredient(ItemID.BandofRegeneration, 1)
            .AddTile(TileID.Anvils)
            .Register();
        }*/
    }
}
