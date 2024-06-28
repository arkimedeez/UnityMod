using arkimedeezMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace arkimedeezMod.Items.Armor.SanguineArmor
{
    // The AutoloadEquip attribute automatically attaches an equip texture to this item.
    // Providing the EquipType.Body value here will result in TML expecting X_Arms.png, X_Body.png and X_FemaleBody.png sprite-sheet files to be placed next to the item's main texture.
    [AutoloadEquip(EquipType.Body)]
    public class SanguineChestplate : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 34; // Width of the item
            Item.height = 22; // Height of the item
            Item.value = Item.sellPrice(gold: 1, silver: 50);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 10; // The amount of defense the item will give when equipped
        }

        public override void UpdateEquip(Player player)
        {
            player.endurance += 0.05f;
            player.lifeRegen += 1;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<HeavengoldBar>(12)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}