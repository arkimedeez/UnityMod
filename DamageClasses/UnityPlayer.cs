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
                    OmegaWeaponHeldTimer = OmegaWeaponHeldTimer + 2;
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
                Player.SetImmuneTimeForAllTypes(Player.longInvince ? 1 : 1);
                DodgeTimer--;
            }

            if (Player.HasBuff(ModContent.BuffType<SpeedI>()))
            {
                Player.moveSpeed += 0.15f;
                Player.maxRunSpeed += 0.15f;
                Player.runAcceleration += 0.15f;
                Player.accRunSpeed += 0.15f;
            } 
            else if (Player.HasBuff(ModContent.BuffType<SpeedII>()))
            {
                Player.moveSpeed *= 1.3f;
                Player.maxRunSpeed *= 1.3f;
            }
            if (Player.HasBuff(ModContent.BuffType<SpeedIII>()))
            {
                Player.moveSpeed += 0.45f;
                Player.maxRunSpeed += 0.45f;
                Player.runAcceleration += 0.45f;
                Player.accRunSpeed += 0.45f;
            }
            if (Player.HasBuff(ModContent.BuffType<SpeedIV>()))
            {
                Player.moveSpeed += 0.6f;
                Player.maxRunSpeed += 0.6f;
                Player.runAcceleration += 0.6f;
                Player.accRunSpeed += 0.6f;
            }
            if (Player.HasBuff(ModContent.BuffType<SpeedV>()))
            {
                Player.moveSpeed += 0.75f;
                Player.maxRunSpeed += 0.75f;
                Player.runAcceleration += 0.75f;
                Player.accRunSpeed += 0.75f;
            }
        }

        /*public override void PostUpdateRunSpeeds()
        {

        }*/
    }
}