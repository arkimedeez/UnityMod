﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

//Regeneration buff, level one
namespace arkimedeezMod.Buffs
{
    public class ShatteredArmor : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;  // Is it a debuff?
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.endurance -= 0.4f;
        }
    }
}