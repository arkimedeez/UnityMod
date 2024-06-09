using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace arkimedeezMod.Buffs
{
    public class CorimsWarlockRevolverAbilityCooldown : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            Main.debuff[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
        }
    }
}