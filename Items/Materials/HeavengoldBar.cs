using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace arkimedeezMod.Items.Materials
{
    public class HeavengoldBar : ModItem
    {
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Orange; // The color that the item's name will be in-game.
            Item.value = Item.sellPrice(silver: 20);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.FallenStar)
            .AddIngredient(ItemID.Bone, 3)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
