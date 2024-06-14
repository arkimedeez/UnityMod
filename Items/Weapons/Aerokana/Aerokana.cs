using Microsoft.Xna.Framework;
using Terraria;
using arkimedeezMod.Items.Materials;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using arkimedeezMod.DamageClasses;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using System.Security.Cryptography.X509Certificates;

namespace arkimedeezMod.Items.Weapons.Aerokana
{
    // ExampleCustomSwingSword is an example of a sword with a custom swing using a held projectile
    // This is great if you want to make melee weapons with complex swing behavior
    public class Aerokana : ModItem
    {
        public int attackType = 2; // keeps track of which attack it is
        public int comboExpireTimer = 0; // we want the attack pattern to reset if the weapon is not used for certain period of time

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;
            Item.value = Item.sellPrice(gold: 1, silver: 50);
            Item.rare = ItemRarityID.Orange;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.crit = 9;
            Item.shootSpeed = 0;
            Item.knockBack = 7;  // The knockback of your sword, this is dynamically adjusted in the projectile code.
            Item.autoReuse = true; // This determines whether the weapon has autoswing
            Item.damage = 25; // The damage of your sword, this is dynamically adjusted in the projectile code.
            Item.DamageType = ModContent.GetInstance<OmegaDamage>();
            Item.noMelee = true;  // This makes sure the item does not deal damage from the swinging animation
            Item.noUseGraphic = true; // This makes sure the item does not get shown when the player swings his hand

            Item.shoot = ModContent.ProjectileType<AerokanaProjectile>(); // The sword as a projectile
        }


        public override bool AltFunctionUse(Player player) => true;

        public bool RightClickAbility = false;

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                RightClickAbility = false; 

                Item.UseSound = SoundID.Item1;
                Item.shoot = ModContent.ProjectileType<AerokanaProjectile>(); // The sword as a projectile
                return base.CanUseItem(player);
            }
            else
            { //AerokanaSlash
                if (UnityPlayer.OmegaChargeCurrent == 100)
                {
                    RightClickAbility = true;

                    Item.UseSound = new SoundStyle($"{nameof(arkimedeezMod)}/Assets/Audio/SwordSlash2");
                    Item.shoot = ModContent.ProjectileType<AerokanaProjectile>(); // The sword as a projectile

                    Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
                    player.velocity = (target - player.position) / 20;

                    UnityPlayer.DodgeTimer += 100;
                    UnityPlayer.OmegaChargeCurrent = 0;
                }
                else
                {
                    Item.shoot = ProjectileID.None;
                    Item.UseSound = SoundID.Item1;
                }
                return base.CanUseItem(player);
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, type, damage * (RightClickAbility ? 5 : 1), knockback, Main.myPlayer, attackType);
            attackType = 2; // Increment attackType to make sure next swing is different
            comboExpireTimer = 0; // Every time the weapon is used, we reset this so the combo does not expire
            return false; // return false to prevent original projectile from being shot
        }

        public override bool MeleePrefix() => true; // return true to allow weapon to have melee prefixes (e.g. Legendary)

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<HeavengoldBar>(10)
            .AddIngredient(ItemID.Muramasa, 1)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}