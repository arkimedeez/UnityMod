using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

//Speed increase buff, level one
namespace arkimedeezMod.Buffs.StatBoosts
{
    public class DefenseII : ModBuff
    {
        public override LocalizedText Description => base.Description.WithFormatArgs("Increased defense");

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 20;
        }
    }
}