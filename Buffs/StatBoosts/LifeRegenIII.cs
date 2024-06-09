using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

//Regeneration buff, level three
namespace arkimedeezMod.Buffs.StatBoosts
{
    public class LifeRegenIII : ModBuff
    {
        public static readonly int lifeRegen = 7;

        public override LocalizedText Description => base.Description.WithFormatArgs(lifeRegen);

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += lifeRegen;
        }
    }
}