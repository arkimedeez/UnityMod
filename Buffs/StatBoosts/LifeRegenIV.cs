using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

//Regeneration buff, level four
namespace arkimedeezMod.Buffs.StatBoosts
{
    public class LifeRegenIV : ModBuff
    {
        public static readonly int lifeRegen = 9;

        public override LocalizedText Description => base.Description.WithFormatArgs(lifeRegen);

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += lifeRegen;
        }
    }
}