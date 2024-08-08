using arkimedeezMod.Buffs;
using arkimedeezMod.Buffs.StatBoosts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace arkimedeezMod.DamageClasses
{
    public class PlayerEvents : ModPlayer
    {
        public bool UnholyArtifact;
        public bool BloodlustDagger;
        public bool DemonicVengance;

        public override void ResetEffects()
        {
            UnholyArtifact = false;
            BloodlustDagger = false;
            DemonicVengance = false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (BloodlustDagger)
            {
                target.AddBuff(ModContent.BuffType<BoilingBlood>(), 300);
            }
        }

        public override void PostHurt(Player.HurtInfo info)
        {
            if (UnholyArtifact)
            {
                Player.AddBuff(ModContent.BuffType<ShatteredArmor>(), 600);
                return;
            }

            if (DemonicVengance)
            {
                if(info.Damage > (int)(Player.statLifeMax / 20))
                {
                    Player.AddBuff(ModContent.BuffType<DemonicEnhancing>(), 300);
                    UnityPlayer.OmegaChargeCurrent = UnityPlayer.OmegaChargeCurrent + 10;
                }
            }
        }

        public override void PostUpdateMiscEffects()
        {
            UpdateSpeedEffect(Player);
        }

        public void UpdateSpeedEffect(Player player)
        {
            if (player.HasBuff(ModContent.BuffType<SpeedII>()))
            {
                player.moveSpeed *= 1.3f;
                player.maxRunSpeed *= 1.3f;
            }
        }
    }


    //Omega charge system
    public class UnityPlayer : ModPlayer
    {
        public static float OmegaChargeCurrent;
        //public const int DefaultOmegaChargeMax = 100;
        public static readonly Color HealExampleResource = new(187, 91, 201);

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
                Player.SetImmuneTimeForAllTypes(1); // Player.longInvince ? 1 : 1  thats its always 1
                DodgeTimer--;
            }
        }
    }
}