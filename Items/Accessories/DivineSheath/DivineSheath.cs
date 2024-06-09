using arkimedeezMod.DamageClasses;
using Terraria;
using Terraria.ID;
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
            float bonus = UnityPlayer.OmegaWeaponHeldTimer > 0 ? UnityPlayer.OmegaChargeCurrent / 7 : 0;
            player.GetDamage(DamageClass.Generic) += (5 + bonus) / 100f;
        }
    }
}
