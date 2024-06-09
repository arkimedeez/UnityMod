using arkimedeezMod.Buffs.StatBoosts;
using arkimedeezMod.DamageClasses;
using arkimedeezMod.Projectiles;
using Microsoft.Xna.Framework;
using MonoMod.Logs;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace arkimedeezMod.Items.Weapons.JoiseMegaphone
{
    public class JoiseMegaphone : ModItem
    {
        public override void SetDefaults()
        {
            // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

            // Common Properties
            Item.width = 62; // Hitbox width of the item.
            Item.height = 32; // Hitbox height of the item.
            Item.scale = 0.75f;
            Item.value = Item.sellPrice(gold: 1, silver: 50);
            Item.rare = ItemRarityID.Orange;

            // Use Properties
            Item.useTime = 64; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 64; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
            Item.consumeAmmoOnLastShotOnly = true;

            // Weapon Properties
            Item.DamageType = ModContent.GetInstance<OmegaDamage>();
            Item.damage = 100; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.knockBack = 5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.noMelee = true; // So the item's animation doesn't do damage.
            Item.crit = 5;

            // Gun Properties
            Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
            Item.shootSpeed = 4f; // The speed of the projectile (measured in pixels per frame).
            Item.shoot = ModContent.ProjectileType<MegaphoneProjectile>();
            Item.UseSound = new SoundStyle($"{nameof(arkimedeezMod)}/Assets/Audio/megaphone1")
            {
                Volume = 0.5f,
                PitchVariance = 0.2f,
                MaxInstances = 3,
            };
        }

        public override bool AltFunctionUse(Player player) => true;

        public int ShootType = 0;

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                ShootType = 0;

                Item.useTime = 64; // The item's use time in ticks (60 ticks == 1 second.)
                Item.useAnimation = 64; // The length of the item's use animation in ticks (60 ticks == 1 second.)
                Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
                Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
                Item.consumeAmmoOnLastShotOnly = true;

                // Weapon Properties
                Item.DamageType = ModContent.GetInstance<OmegaDamage>();
                Item.damage = 100; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
                Item.knockBack = 5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
                Item.noMelee = true; // So the item's animation doesn't do damage.
                Item.crit = 5;

                // Gun Properties
                Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
                Item.shootSpeed = 4f; // The speed of the projectile (measured in pixels per frame).
                Item.shoot = ModContent.ProjectileType<MegaphoneProjectile>(); // The sword as a projectile

                if (ShootType == 1)
                {
                    Item.UseSound = new SoundStyle($"{nameof(arkimedeezMod)}/Assets/Audio/VineBoom")
                    {
                        Volume = 10f,
                        PitchVariance = 0f,
                        MaxInstances = 3,
                    };
                }
                else
                {
                    int sound = 1;
                    sound = Main.rand.Next(1, 6);
                    if (sound == 1)
                    {
                        Item.UseSound = new SoundStyle($"{nameof(arkimedeezMod)}/Assets/Audio/megaphone1")
                        {
                            Volume = 0.5f,
                            PitchVariance = 0.2f,
                            MaxInstances = 3,
                        };
                    }
                    else if (sound == 2)
                    {
                        Item.UseSound = new SoundStyle($"{nameof(arkimedeezMod)}/Assets/Audio/megaphone2")
                        {
                            Volume = 0.5f,
                            PitchVariance = 0.2f,
                            MaxInstances = 3,
                        };
                    }
                    else if (sound == 3)
                    {
                        Item.UseSound = new SoundStyle($"{nameof(arkimedeezMod)}/Assets/Audio/megaphone3")
                        {
                            Volume = 0.5f,
                            PitchVariance = 0.2f,
                            MaxInstances = 3,
                        };
                    }
                    else if (sound == 4)
                    {
                        Item.UseSound = new SoundStyle($"{nameof(arkimedeezMod)}/Assets/Audio/megaphone4")
                        {
                            Volume = 0.5f,
                            PitchVariance = 0.2f,
                            MaxInstances = 3,
                        };
                    }
                    else
                    {
                        Item.UseSound = new SoundStyle($"{nameof(arkimedeezMod)}/Assets/Audio/megaphone5")
                        {
                            Volume = 0.5f,
                            PitchVariance = 0.2f,
                            MaxInstances = 3,
                        };
                    };
                }
                return base.CanUseItem(player);
            }
            else
            {
                if (UnityPlayer.OmegaChargeCurrent == 100)
                {
                    ShootType = 1;
                    
                    Item.useTime = 64; // The item's use time in ticks (60 ticks == 1 second.)
                    Item.useAnimation = 64; // The length of the item's use animation in ticks (60 ticks == 1 second.)
                    Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
                    Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
                    Item.consumeAmmoOnLastShotOnly = true;

                    // Weapon Properties
                    Item.DamageType = ModContent.GetInstance<OmegaDamage>();
                    Item.damage = 100;
                    Item.knockBack = 5f;
                    Item.noMelee = true;
                    Item.crit = 5;
                    Item.shootSpeed = 30f;
                    Item.shoot = ModContent.ProjectileType<MegaphoneProjectile>();
                    UnityPlayer.OmegaChargeCurrent = 0;
                    player.statLife -= 100;
                    if (ShootType == 1)
                    {
                        Item.UseSound = new SoundStyle($"{nameof(arkimedeezMod)}/Assets/Audio/VineBoom")
                        {
                            Volume = 10f,
                            PitchVariance = 0f,
                            MaxInstances = 3,
                        };
                    }
                    else
                    {
                        int sound = 1;
                        sound = Main.rand.Next(1, 6);
                        if (sound == 1)
                        {
                            Item.UseSound = new SoundStyle($"{nameof(arkimedeezMod)}/Assets/Audio/megaphone1")
                            {
                                Volume = 0.5f,
                                PitchVariance = 0.2f,
                                MaxInstances = 3,
                            };
                        }
                        else if (sound == 2)
                        {
                            Item.UseSound = new SoundStyle($"{nameof(arkimedeezMod)}/Assets/Audio/megaphone2")
                            {
                                Volume = 0.5f,
                                PitchVariance = 0.2f,
                                MaxInstances = 3,
                            };
                        }
                        else if (sound == 3)
                        {
                            Item.UseSound = new SoundStyle($"{nameof(arkimedeezMod)}/Assets/Audio/megaphone3")
                            {
                                Volume = 0.5f,
                                PitchVariance = 0.2f,
                                MaxInstances = 3,
                            };
                        }
                        else if (sound == 4)
                        {
                            Item.UseSound = new SoundStyle($"{nameof(arkimedeezMod)}/Assets/Audio/megaphone4")
                            {
                                Volume = 0.5f,
                                PitchVariance = 0.2f,
                                MaxInstances = 3,
                            };
                        }
                        else
                        {
                            Item.UseSound = new SoundStyle($"{nameof(arkimedeezMod)}/Assets/Audio/megaphone5")
                            {
                                Volume = 0.5f,
                                PitchVariance = 0.2f,
                                MaxInstances = 3,
                            };
                        };
                    }
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
            if (ShootType == 0)
            {
                int v = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer);
                return false; // return false to prevent original projectile from being shot
            }
            else
            {
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<MegaphoneProjectileAlt1>(), damage * 10, knockback, Main.myPlayer);
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<MegaphoneProjectileAlt2>(), damage * 10, knockback, Main.myPlayer);
                player.velocity = -velocity;
                return false;
            }
        }


        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

        // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-15f, 2f);
        }

        // How can I make the shots appear out of the muzzle exactly?
        // Also, when I do this, how do I prevent shooting through tiles?
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;

            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<Materials.HeavengoldBar>(14)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}