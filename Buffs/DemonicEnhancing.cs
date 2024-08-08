using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

//Regeneration buff, level one
namespace arkimedeezMod.Buffs
{
    public class DemonicEnhancing : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 15 / 100f;
            player.statDefense += 10;
        }
    }
}