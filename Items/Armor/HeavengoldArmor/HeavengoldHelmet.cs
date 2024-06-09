using arkimedeezMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace arkimedeezMod.Items.Armor.HeavengoldArmor
{
    // The AutoloadEquip attribute automatically attaches an equip texture to this item.
    // Providing the EquipType.Body value here will result in TML expecting X_Arms.png, X_Body.png and X_FemaleBody.png sprite-sheet files to be placed next to the item's main texture.
    [AutoloadEquip(EquipType.Head)]
    public class HeavengoldHelmet : ModItem
    {
        public int GenericDamageBonus = 7;
        public int MovmentSpeedBonusPercent = 10;
        public int MaxMinionBonus = 2;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(GenericDamageBonus);
        public override void SetDefaults()
        {
            Item.width = 34; // Width of the item
            Item.height = 22; // Height of the item
            Item.value = Item.sellPrice(gold: 1, silver: 50);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 3; // The amount of defense the item will give when equipped
        }

        public static LocalizedText SetBonusText { get; private set; }

        public override void SetStaticDefaults()
        {
            SetBonusText = this.GetLocalization("SetBonus",);
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) *= 1 * (GenericDamageBonus/100f); //7%
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<HeavengoldChestplate>() && legs.type == ModContent.ItemType<HeavengoldLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = SetBonusText.Value;
            player.maxMinions += 2;
            player.manaRegenBonus += 80;
            player.GetAttackSpeed<MeleeDamageClass>() += 0.15f;
            player.GetCritChance<RangedDamageClass>() += 15;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<HeavengoldBar>(8)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}