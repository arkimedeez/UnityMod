using Terraria;
using Terraria.ModLoader;

namespace arkimedeezMod.DamageClasses
{
    public class OmegaDamage : DamageClass
    {
        // This is an example damage class designed to demonstrate all the current functionality of the feature and explain how to create one of your own, should you need one.
        // For information about how to apply stat bonuses to specific damage classes, please instead refer to ExampleMod/Content/Items/Accessories/ExampleStatBonusAccessory.
        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            if (damageClass == DamageClass.Generic)
                return StatInheritanceData.Full;
            if (damageClass == DamageClass.Melee)
                return StatInheritanceData.Full;
            if (damageClass == DamageClass.Magic)
                return StatInheritanceData.Full;
            if (damageClass == DamageClass.Ranged)
                return StatInheritanceData.Full;
            if (damageClass == DamageClass.Summon)
                return StatInheritanceData.Full;
            if (damageClass == DamageClass.Throwing)
                return StatInheritanceData.Full;

            return new StatInheritanceData(
                damageInheritance: 1f,
                critChanceInheritance: 1f,
                attackSpeedInheritance: 1f,
                armorPenInheritance: 1f,
                knockbackInheritance: 1f
            );
        }
        public override bool UseStandardCritCalcs => true;

        public override bool GetEffectInheritance(DamageClass damageClass)
        {
            // This method allows you to make your damage class benefit from and be able to activate other classes' effects (e.g. Spectre bolts, Magma Stone) based on what returns true.
            // Note that unlike our stat inheritance methods up above, you do not need to account for universal bonuses in this method.
            // For this example, we'll make our class able to activate melee- and magic-specifically effects.
            if (damageClass == DamageClass.Melee)
                return true;
            if (damageClass == DamageClass.Magic)
                return true;
            if (damageClass == DamageClass.Ranged)
                return true;
            if (damageClass == DamageClass.Summon)
                return true;

            return false;
        }

    }
}