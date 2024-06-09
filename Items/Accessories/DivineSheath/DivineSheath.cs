using arkimedeezMod.DamageClasses;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace arkimedeezMod.Items.Accessories.DivineSheath
{
    public class DivineSheath : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 38;
            Item.accessory = true;
            Item.value = Item.sellPrice(gold: 1, silver: 50);
            Item.rare = ItemRarityID.Orange;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (UnityPlayer.OmegaWeaponHeldTimer > 0)
            {
                player.GetDamage(DamageClass.Generic) += (5 + UnityPlayer.OmegaChargeCurrent / 7) / 100f;
            }
            else
            {
                player.GetDamage(DamageClass.Generic) += 5 / 100f;
            }
        }
    }
}
