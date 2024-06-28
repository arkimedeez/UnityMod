using arkimedeezMod.DamageClasses;
using arkimedeezMod.Items.Materials;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace arkimedeezMod.Items.Accessories.UnholyArtifact
{
    public class UnholyArtifact : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 52;
            Item.accessory = true;
            Item.value = Item.sellPrice(gold: 1, silver: 50);
            Item.rare = ItemRarityID.Orange;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 15;
            player.GetModPlayer<PlayerEvents>().UnholyArtifact = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<HeavengoldBar>(6)
            .AddIngredient(ItemID.BandofRegeneration, 1)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
