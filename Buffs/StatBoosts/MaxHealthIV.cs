using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

//Regeneration buff, level one
namespace arkimedeezMod.Buffs.StatBoosts
{
    public class MaxHealthIV : ModBuff
    {
        public static readonly int MaxHealth = 160;

        public override LocalizedText Description => base.Description.WithFormatArgs(MaxHealth);

        public override void Update(Player player, ref int buffIndex)
        {
            player.statLifeMax2 += MaxHealth;
        }
    }
}