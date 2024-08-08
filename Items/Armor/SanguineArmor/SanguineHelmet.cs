using arkimedeezMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace arkimedeezMod.Items.Armor.SanguineArmor
{
    // The AutoloadEquip attribute automatically attaches an equip texture to this item.
    // Providing the EquipType.Body value here will result in TML expecting X_Arms.png, X_Body.png and X_FemaleBody.png sprite-sheet files to be placed next to the item's main texture.
    [AutoloadEquip(EquipType.Head)]
    public class SanguineHelmet : ModItem
    {
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs("7% increased damage");
        public override void SetDefaults()
        {
            Item.width = 34; // Width of the item
            Item.height = 22; // Height of the item
            Item.value = Item.sellPrice(gold: 1, silver: 50);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 6; // The amount of defense the item will give when equipped
        }

        public static LocalizedText SetBonusText { get; private set; }

        public override void SetStaticDefaults()
        {
            SetBonusText = this.GetLocalization("SetBonus");
        }

        public override void UpdateEquip(Player player)
        {
            player.lifeRegen += 1;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<SanguineChestplate>() && legs.type == ModContent.ItemType<SanguineLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = SetBonusText.Value;
            player.lifeRegen += 4;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<ClothiersDelight>(10)
            .AddIngredient(ItemID.MoltenHelmet)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}