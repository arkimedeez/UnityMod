using arkimedeezMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace arkimedeezMod.Items.Armor.SanguineArmor
{
    // The AutoloadEquip attribute automatically attaches an equip texture to this item.
    // Providing the EquipType.Body value here will result in TML expecting X_Arms.png, X_Body.png and X_FemaleBody.png sprite-sheet files to be placed next to the item's main texture.
    [AutoloadEquip(EquipType.Legs)]
    public class SanguineLeggings : ModItem
    {
        public int MovmentSpeedBonusPercent = 10;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MovmentSpeedBonusPercent);
        public override void SetDefaults()
        {
            Item.width = 34; // Width of the item
            Item.height = 22; // Height of the item
            Item.value = Item.sellPrice(gold: 1, silver: 50);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 9; // The amount of defense the item will give when equipped
        }

        public override void UpdateEquip(Player player)
        {

        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<ClothiersDelight>(10)
            .AddIngredient(ItemID.MoltenGreaves)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}