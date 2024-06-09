using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

//Regeneration buff, level five
namespace arkimedeezMod.Buffs.StatBoosts
{
    public class LifeRegenV : ModBuff
    {
        public static readonly int lifeRegen = 11;

        public override LocalizedText Description => base.Description.WithFormatArgs(lifeRegen);

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += lifeRegen;
        }
    }
}