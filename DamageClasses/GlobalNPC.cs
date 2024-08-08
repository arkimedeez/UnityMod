using arkimedeezMod.Buffs;
using arkimedeezMod.Buffs.StatBoosts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using arkimedeezMod.Items.Materials;

namespace arkimedeezMod.DamageClasses
{
    public class NPCEvents : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public bool BoilingBlood;

        public override void ResetEffects(NPC NPC)
        {
            BoilingBlood = false;
        }

        public override void UpdateLifeRegen(NPC NPC, ref int damage)
        {
            if (BoilingBlood)
                NPC.lifeRegen -= 16;
        }
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.SkeletronHead)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ClothiersDelight>(), 1, 20, 60));
            }
        }
    }
}