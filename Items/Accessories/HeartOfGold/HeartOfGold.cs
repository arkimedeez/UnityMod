using arkimedeezMod.DamageClasses;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace arkimedeezMod.Items.Accessories.HeartOfGold
{
    public class HeartOfGold : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 32;
            Item.accessory = true;
            Item.value = Item.sellPrice(gold: 1, silver: 50);
            Item.rare = ItemRarityID.Orange;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (UnityPlayer.OmegaWeaponHeldTimer > 0)
            {
                player.GetDamage(DamageClass.Generic) += 15 / 100f;
                player.lifeRegen *= Convert.ToInt32(Math.Round(player.lifeRegen * 0.5));
            }
        }
    }
}
