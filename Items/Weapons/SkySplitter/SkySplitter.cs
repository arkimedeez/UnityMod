using arkimedeezMod.Buffs.StatBoosts;
using arkimedeezMod.DamageClasses;
using arkimedeezMod.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace arkimedeezMod.Items.Weapons.SkySplitter
{
    public class SkySplitter : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.SkipsInitialUseSound[Item.type] = true; // This skips use animation-tied sound playback, so that we're able to make it be tied to use time instead in the UseItem() hook.
            ItemID.Sets.Spears[Item.type] = true; // This allows the game to recognize our new item as a spear.
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = false;
        }

        public override void SetDefaults()
        {
            // Common Properties

            Item.Size = new(128,128);
            Item.value = Item.sellPrice(gold: 1, silver: 50);
            Item.rare = ItemRarityID.Orange;

            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.crit = 9;
            Item.channel = true;

            // Weapon Properties
            Item.knockBack = 7;  // The knockback of your sword, this is dynamically adjusted in the projectile code.
            Item.autoReuse = false; // This determines whether the weapon has autoswing
            Item.damage = 9; // The damage of your sword, this is dynamically adjusted in the projectile code.
            Item.DamageType = ModContent.GetInstance<OmegaDamage>();
            Item.noMelee = true;  // This makes sure the item does not deal damage from the swinging animation
            Item.noUseGraphic = true; // This makes sure the item does not get shown when the player swings his hand
            Item.shoot = ModContent.ProjectileType<SkySplitterProjectile>();
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
        }




        public override bool AltFunctionUse(Player player) => true;

        public bool RightClickAbility = false;

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                RightClickAbility = false;
                Item.useTime = 40;
                Item.useAnimation = 40;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.channel = true;
                Item.autoReuse = false; // This determines whether the weapon has autoswing
                Item.shoot = ModContent.ProjectileType<SkySplitterProjectile>();
                Item.shootSpeed = 0;
                Item.UseSound = SoundID.Item1;
                return base.CanUseItem(player);
            }
            else
            {
                if (UnityPlayer.OmegaChargeCurrent == 100)
                {
                    RightClickAbility = true;
                    Item.shootSpeed = 1000f;
                    Item.shoot = ModContent.ProjectileType<SkySplitterSpear>();
                    Item.autoReuse = true;
                    Item.useAnimation = 18; // The length of the item's use animation in ticks (60 ticks == 1 second.)
                    Item.useTime = 18; // The length of the item's use time in ticks (60 ticks == 1 second.)
                    Item.UseSound = new SoundStyle($"{nameof(arkimedeezMod)}/Assets/Audio/SwordSlash2");
                    Item.useStyle = ItemUseStyleID.Thrust;
                    Item.channel = false;
                    UnityPlayer.OmegaChargeCurrent = 0;
                    player.AddBuff(ModContent.BuffType<SpeedII>(), 1200);
                } 
                else
                {
                    Item.shoot = ProjectileID.None;
                    Item.UseSound = SoundID.Item1;
                }
                return base.CanUseItem(player);
            }
        }


        public override bool? UseItem(Player player)
        {
            // Because we're skipping sound playback on use animation start, we have to play it ourselves whenever the item is actually used.
            if (!Main.dedServ && Item.UseSound.HasValue)
            {
                SoundEngine.PlaySound(Item.UseSound.Value, player.Center);
            }

            return null;
        }
        public override bool MeleePrefix() => true; // return true to allow weapon to have melee prefixes (e.g. Legendary)

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(1));
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if(RightClickAbility == true)
            {
                Projectile.NewProjectileDirect(Projectile.GetSource_None(), position, velocity, ModContent.ProjectileType<SkySplitterProjectileAlt>(), damage*25, knockback);

                return base.Shoot(player, source, position, velocity, type, damage*25, knockback);
            }
            else
            {
                return true;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<Materials.HeavengoldBar>(10)
            .AddIngredient(ItemID.Bone, 15)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}