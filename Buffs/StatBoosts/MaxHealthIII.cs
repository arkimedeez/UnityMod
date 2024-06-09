using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

//Increased maximum health buff, level one
namespace arkimedeezMod.Buffs.StatBoosts
{
    public class MaxHealthIII : ModBuff
    {
        public static readonly int MaxHealth = 120;

        public override LocalizedText Description => base.Description.WithFormatArgs(MaxHealth);

        public override void Update(Player player, ref int buffIndex)
        {
            player.statLifeMax2 += MaxHealth;
        }
    }
}