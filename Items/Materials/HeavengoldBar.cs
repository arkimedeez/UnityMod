using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace arkimedeezMod.Items.Materials
{
    public class HeavengoldBar : ModItem
    {
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.LightRed; // The color that the item's name will be in-game.
            Item.value = Item.sellPrice(silver: 20);
            Item.maxStack = 9999;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.PalladiumBar)
            .AddIngredient(ItemID.SoulofLight)
            .AddIngredient(ItemID.FallenStar)
            .AddTile(TileID.MythrilAnvil)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemID.CobaltBar)
            .AddIngredient(ItemID.SoulofLight)
            .AddIngredient(ItemID.FallenStar)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}
