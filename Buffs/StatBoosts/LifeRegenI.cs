using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

//Regeneration buff, level one
namespace arkimedeezMod.Buffs.StatBoosts
{
    public class LifeRegenI : ModBuff
    {
        public static readonly int lifeRegen = 3;

        public override LocalizedText Description => base.Description.WithFormatArgs(lifeRegen);

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += lifeRegen;
        }
    }
}