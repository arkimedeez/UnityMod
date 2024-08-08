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
        public int DamageBonus = 7;
        public int MovmentSpeedBonusPercent = 10;
        public int MeleeAttackSpeedBonus = 15;
        public int RangedCritBonus = 15;
        public int MaxMinionBonus = 2;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBonus);
        public override void SetDefaults()
        {
            Item.width = 34; // Width of the item
            Item.height = 22; // Height of the item
            Item.value = Item.sellPrice(gold: 1, silver: 50);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 6; // The amount of defense the item will give when equipped
        }

        public static LocalizedText SetBonusText { get; private set; }

        public override void SetStaticDefaults()
        {
            SetBonusText = this.GetLocalization("SetBonus").WithFormatArgs(MaxMinionBonus, MeleeAttackSpeedBonus, RangedCritBonus);
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.1f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<HeavengoldChestplate>() && legs.type == ModContent.ItemType<HeavengoldLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = SetBonusText.Value;
            player.maxMinions += MaxMinionBonus;
            player.manaRegenBonus += 120;
            player.GetAttackSpeed<MeleeDamageClass>() += MeleeAttackSpeedBonus / 100f;
            player.GetCritChance<RangedDamageClass>() += RangedCritBonus;
            player.GetDamage(DamageClass.Generic) += 0.15f;
            player.GetDamage(DamageClass.Throwing) += 0.15f;
            if (ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod))
            {
                if (thoriumMod.TryFind("BardDamage", out DamageClass BardDamage))
                {
                    player.GetDamage(BardDamage) += 0.15f;
                }
                if (thoriumMod.TryFind("HealerDamage", out DamageClass HealerDamage))
                {
                    player.GetDamage(HealerDamage) += 0.15f;
                }
            }

            if (ModLoader.TryGetMod("ClickerClass", out Mod clickerClass))
            {
                if (clickerClass.TryFind("ClickerDamage", out DamageClass ClickerDamage))
                {
                    player.GetDamage(ClickerDamage) += 0.15f;
                }
            }

            if (ModLoader.TryGetMod("OrchidMod", out Mod orchidMod))
            {
                if (orchidMod.TryFind("GuardianDamageClass", out DamageClass GuardianDamage))
                {
                    player.GetDamage(GuardianDamage) += 0.15f;
                }

                if (orchidMod.TryFind("GamblerDamageClass", out DamageClass GamblerDamage))
                {
                    player.GetDamage(GamblerDamage) += 0.15f;
                }

                if (orchidMod.TryFind("AlchemistDamageClass", out DamageClass AlchemistDamage))
                {
                    player.GetDamage(AlchemistDamage) += 0.15f;
                }

                if (orchidMod.TryFind("ShamanDamageClass", out DamageClass ShamanDamage))
                {
                    player.GetDamage(ShamanDamage) += 0.15f;
                }
            }

            if (ModLoader.TryGetMod("MetroidMod", out Mod metroidMod))
            {
                if (metroidMod.TryFind("HunterDamageClass", out DamageClass HunterDamage))
                {
                    player.GetDamage(HunterDamage) += 0.15f;
                }
            }

            if (ModLoader.TryGetMod("VitalityMod", out Mod vitalityMod))
            {
                if (vitalityMod.TryFind("BloodHunterClass", out DamageClass BloodDamage))
                {
                    player.GetDamage(BloodDamage) += 0.15f;
                }
            }
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