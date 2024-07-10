using arkimedeezMod.Buffs;
using arkimedeezMod.Buffs.StatBoosts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace arkimedeezMod.DamageClasses
{
    public class ItemDirectionPlayer : ModPlayer
    {
        public float swordDirection = 1f;
    }

    public class PlayerEvents : ModPlayer
    {
        public bool UnholyArtifact;
        public bool BloodlustDagger;

        public override void ResetEffects()
        {
            UnholyArtifact = false;
            BloodlustDagger = false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (BloodlustDagger)
            {
                target.AddBuff(BuffID.Frostburn, 300);
            }
        }

        public override void PostHurt(Player.HurtInfo info)
        {
            if (UnholyArtifact)
            {
                Player.AddBuff(ModContent.BuffType<ShatteredArmor>(), 600);
                return;
            }
        }
    }

    public class UnityPlayer : ModPlayer
    {
        // Here we create a custom resource, similar to mana or health.
        // Creating some variables to define the current value of our example resource as well as the current maximum value. We also include a temporary max value, as well as some variables to handle the natural regeneration of this resource.
        public static float OmegaChargeCurrent; // Current value of our example resource
        public const int DefaultOmegaChargeMax = 100; // Default maximum value of example resource
        public static readonly Color HealExampleResource = new(187, 91, 201); // We can use this for CombatText, if you create an item that replenishes exampleResourceCurrent.

        public bool TextShown = false;
        public static int DodgeTimer = 0;
        public static int OmegaWeaponHeldTimer = 0;

        public override void PostUpdateMiscEffects()
        {
            if (Main.LocalPlayer.HeldItem.CountsAsClass(ModContent.GetInstance<OmegaDamage>()))
            {
                if(OmegaWeaponHeldTimer < 900)
                {
                    OmegaWeaponHeldTimer += 2;
                }
            }
            else
            {
                if (OmegaWeaponHeldTimer > 0)
                {
                    OmegaWeaponHeldTimer--;
                }
            }

            if (OmegaChargeCurrent >= 100)
            {
                if (TextShown == false)
                {
                    OmegaChargeCurrent = 100;
                    CombatText.NewText(Player.Hitbox, new Color(75, 0, 130), "Omega Charged!", true);
                    SoundEngine.PlaySound(SoundID.Item6);
                    TextShown = true;
                }
                OmegaChargeCurrent = 100;
            }
            else
            {
                TextShown = false;
            }

            if (DodgeTimer > 0)
            {
                Player.SetImmuneTimeForAllTypes(1); // Player.longInvince ? 1 : 1  thats its allways 1
                DodgeTimer--;
            }
            UpdateSpeedEffect(Player);
        }

        public void UpdateSpeedEffect(Player player)
        {

            if (player.HasBuff(ModContent.BuffType<SpeedI>())) //why do you no update it on buff effect?
            {
                player.moveSpeed += 0.15f;
                player.maxRunSpeed += 0.15f;
                player.runAcceleration += 0.15f;
                player.accRunSpeed += 0.15f;
            }
            else if (player.HasBuff(ModContent.BuffType<SpeedII>()))
            {
                player.moveSpeed *= 1.3f;
                player.maxRunSpeed *= 1.3f;
            }
            if (player.HasBuff(ModContent.BuffType<SpeedIII>()))
            {
                player.moveSpeed += 0.45f;
                player.maxRunSpeed += 0.45f;
                player.runAcceleration += 0.45f;
                player.accRunSpeed += 0.45f;
            }
            if (player.HasBuff(ModContent.BuffType<SpeedIV>()))
            {
                player.moveSpeed += 0.6f;
                player.maxRunSpeed += 0.6f;
                player.runAcceleration += 0.6f;
                player.accRunSpeed += 0.6f;
            }
            if (player.HasBuff(ModContent.BuffType<SpeedV>()))
            {
                player.moveSpeed += 0.75f;
                player.maxRunSpeed += 0.75f;
                player.runAcceleration += 0.75f;
                player.accRunSpeed += 0.75f;
            }
        }
    }
}