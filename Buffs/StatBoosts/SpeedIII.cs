using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

//Speed increase buff, level one
namespace arkimedeezMod.Buffs.StatBoosts
{
    public class SpeedIII : ModBuff
    {
        public static readonly float Speed = 0.45f;

        public override LocalizedText Description => base.Description.WithFormatArgs(Speed);

        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed += Speed;
        }
    }
}