using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

//Regeneration buff, level two
namespace arkimedeezMod.Buffs.StatBoosts
{
    public class LifeRegenII : ModBuff
    {
        public static readonly int lifeRegen = 40;

        public override LocalizedText Description => base.Description.WithFormatArgs(lifeRegen);

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += lifeRegen;
        }
    }
}