using arkimedeezMod.DamageClasses;
using arkimedeezMod.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using arkimedeezMod.Buffs.StatBoosts;
using arkimedeezMod.Items.Materials;

namespace arkimedeezMod.Items.Weapons.StarSeeker
{
    public class StarSeeker : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 50; // The damage your item deals.
            Item.crit = 15; // The critical strike chance the weapon has. The player, by default, has a 4% critical strike chance.
            Item.knockBack = 6; // The force of knockback of the weapon. Maximum is 20

            Item.useTime = 30;
            Item.useAnimation = 30;

            Item.shoot = ModContent.ProjectileType<StarSeekerSwing>();

            Item.Size = new Vector2(60,60); // its the same that  Item.width = 60; and  Item.height = 60;
            Item.value = Item.sellPrice(gold: 1, silver: 50);
            Item.rare = ItemRarityID.Orange;

            Item.useStyle = ItemUseStyleID.Swing; // The useStyle of the Item.
            Item.autoReuse = true; // Whether the weapon can be used more than once automatically by holding the use button.
            Item.DamageType = ModContent.GetInstance<OmegaDamage>();         
            Item.noMelee = true;

            Item.UseSound = new SoundStyle($"{nameof(arkimedeezMod)}/Assets/Audio/SwordSlash7")
            {
                Volume = 0.5f,
                PitchVariance = 0.05f,
            };
            Item.shootSpeed = 10000000;
        }

        public static int ShootType = 0;

        public override bool AltFunctionUse(Player player) => true;

        public override bool CanUseItem(Player player)
        {

            if (UnityPlayer.OmegaChargeCurrent >= 100f && player.altFunctionUse == 2)
            {
                ShootType = 1;
                Item.useTime = 6;
                UnityPlayer.OmegaChargeCurrent = 0f;
                player.AddBuff(ModContent.BuffType<LifeRegenII>(), 1200);
                Item.UseSound = new SoundStyle($"{nameof(arkimedeezMod)}/Assets/Audio/SwordSlash3")
                {
                    Volume = 2.5f,
                    PitchVariance = 0.05f,
                };
            }
            else
            {
                ShootType = 0;
                Item.useTime = 30;
                Item.UseSound = new SoundStyle($"{nameof(arkimedeezMod)}/Assets/Audio/SwordSlash7")
                {
                    Volume = 0.5f,
                    PitchVariance = 0.1f,
                };
            }

            return base.CanUseItem(player);
        }



        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float adjustedItemScale = player.GetAdjustedItemScale(Item);// Get the melee scale of the player and item.
            Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
            NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI); // Sync the changes in multiplayer.

            float numberProjectiles = ShootType == 0 ? 3 : 64;
            float rotation = MathHelper.ToRadians(ShootType == 0 ? 22 : 180);

            position += Vector2.Normalize(velocity) * 45f;

            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;

                Projectile.NewProjectile(source, position, perturbedSpeed, ModContent.ProjectileType<StarSeekerProjectile>(), damage / (ShootType == 0 ? 2 : 5), knockback);
            }

            return base.Shoot(player, source, position, velocity, type, damage, knockback);

        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(3))
            {
                // Emit dusts when the sword is swung
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.PinkCrystalShard);
            }
        }
        public override bool MeleePrefix() => true;
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<HeavengoldBar>(8)
            .AddIngredient(ItemID.MeteoriteBar, 8)
            .AddIngredient(ItemID.Starfury)
            .AddIngredient(ItemID.BandofRegeneration)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}