using System;
using System.Collections.Generic;

namespace AmteCreator.Internal
{
    public static class ImbuePointCalc
    {
        private static bool IsSkillBonus(int bonusType)
        {
            return (bonusType >= (int)eProperty.Skill_First && bonusType <= (int)eProperty.Skill_Augmentation)
                || (bonusType >= (int)eProperty.Skill_Darkness && bonusType <= (int)eProperty.Skill_Enchantments)
                || (bonusType >= (int)eProperty.Skill_Blades && bonusType <= (int)eProperty.Skill_Last)
                || (bonusType >= (int)eProperty.Skill_Learning && bonusType <= (int)eProperty.Skill_ElementalKnight3);
        }

        private static bool IsToABonus(int bonusType)
        {
            return bonusType >= (int)eProperty.ToABonus_First && bonusType <= (int)eProperty.ToABonus_Last;
        }

        private static bool IsOtherSpecialBonus(int bonusType)
        {
            HashSet<int> specialBonuses = new HashSet<int>
            {
                (int)eProperty.CraftingSkillGain,
                (int)eProperty.RobberyResist,
                (int)eProperty.CraftingSpeed,
                (int)eProperty.CounterAttack,
                (int)eProperty.MythicalOmniRegen,
                (int)eProperty.MythicalTension,
                (int)eProperty.SpellShieldChance,
                (int)eProperty.MaxSpeed,
                (int)eProperty.MythicalSpellReflect,
                (int)eProperty.MaxConcentration,
                (int)eProperty.ArmorFactor,
                (int)eProperty.ArmorAbsorption,
                (int)eProperty.HealthRegenerationRate,
                (int)eProperty.PowerRegenerationRate,
                (int)eProperty.EnduranceRegenerationRate,
                (int)eProperty.SpellRange,
                (int)eProperty.ArcheryRange,
                (int)eProperty.MeleeSpeed,
                (int)eProperty.AllMagicSkills,
                (int)eProperty.AllMeleeWeaponSkills,
                (int)eProperty.AllDualWieldingSkills,
                (int)eProperty.AllArcherySkills,
                (int)eProperty.LivingEffectiveLevel,
                (int)eProperty.EvadeChance,
                (int)eProperty.BlockChance,
                (int)eProperty.ParryChance,
                (int)eProperty.FatigueConsumption,
                (int)eProperty.MeleeDamage,
                (int)eProperty.RangedDamage,
                (int)eProperty.FumbleChance,
                (int)eProperty.MesmerizeDuration,
                (int)eProperty.StunDuration,
                (int)eProperty.SpeedDecreaseDuration,
                (int)eProperty.BladeturnReinforcement,
                (int)eProperty.DefensiveBonus,
                (int)eProperty.SpellFumbleChance,
                (int)eProperty.NegativeReduction,
                (int)eProperty.PieceAblative,
                (int)eProperty.ReactionaryStyleDamage,
                (int)eProperty.SpellPowerCost,
                (int)eProperty.StyleCostReduction,
                (int)eProperty.ToHitBonus,
                (int)eProperty.AllSkills,
                (int)eProperty.WeaponSkill,
                (int)eProperty.CriticalMeleeHitChance,
                (int)eProperty.CriticalArcheryHitChance,
                (int)eProperty.CriticalSpellHitChance,
                (int)eProperty.WaterSpeed,
                (int)eProperty.SpellLevel,
                (int)eProperty.MissHit,
                (int)eProperty.KeepDamage,
                (int)eProperty.DPS,
                (int)eProperty.MagicAbsorption,
                (int)eProperty.CriticalHealHitChance,
                (int)eProperty.MythicalSafeFall,
                (int)eProperty.MythicalDiscumbering,
                (int)eProperty.MythicalCoin,
                (int)eProperty.MythicalCrowdDuration,
                (int)eProperty.LootChance,
                (int)eProperty.BountyPoints,
                (int)eProperty.XpPoints,
                (int)eProperty.Resist_Natural,
                (int)eProperty.ExtraHP,
                (int)eProperty.Conversion,
                (int)eProperty.StyleAbsorb,
                (int)eProperty.RealmPoints,
                (int)eProperty.ArcaneSyphon,
                (int)eProperty.LivingEffectiveness,
                (int)eProperty.RobberyChanceBonus,
                (int)eProperty.RobberyDelayReduction,
                (int)eProperty.StealthEffectivenessBonus,
                (int)eProperty.StealthDetectionBonus,
                (int)eProperty.TensionConservationBonus,
                (int)eProperty.CriticalDotHitChance,
                (int)eProperty.OffhandDamageAndChanceBonus,
                (int)eProperty.OffhandDamageBonus,
                (int)eProperty.OffhandChanceBonus,
                (int)eProperty.DotDamageBonus,
                (int)eProperty.DotDurationDecrease,
                (int)eProperty.MythicalDebuffResistChance,
                (int)eProperty.DamnationEffectEnhancement,
                (int)eProperty.MaxProperty
            };

            return specialBonuses.Contains(bonusType);
        }

        private static bool IsFocusBonus(int bonusType)
        {
            HashSet<int> focusBonuses = new HashSet<int>
            {
                (int)eProperty.Focus_Darkness,
                (int)eProperty.Focus_Suppression,
                (int)eProperty.Focus_Runecarving,
                (int)eProperty.Focus_Spirit,
                (int)eProperty.Focus_Fire,
                (int)eProperty.Focus_Air,
                (int)eProperty.Focus_Cold,
                (int)eProperty.Focus_Earth,
                (int)eProperty.Focus_Light,
                (int)eProperty.Focus_Body,
                (int)eProperty.Focus_Matter,
                (int)eProperty.Focus_Mind,
                (int)eProperty.Focus_Void,
                (int)eProperty.Focus_Mana,
                (int)eProperty.Focus_Enchantments,
                (int)eProperty.Focus_Mentalism,
                (int)eProperty.Focus_Summoning,
                (int)eProperty.Focus_BoneArmy,
                (int)eProperty.Focus_PainWorking,
                (int)eProperty.Focus_DeathSight,
                (int)eProperty.Focus_DeathServant,
                (int)eProperty.Focus_Verdant,
                (int)eProperty.Focus_CreepingPath,
                (int)eProperty.Focus_Arboreal,
                (int)eProperty.Focus_EtherealShriek,
                (int)eProperty.Focus_PhantasmalWail,
                (int)eProperty.Focus_SpectralForce,
                (int)eProperty.Focus_Cursing,
                (int)eProperty.Focus_Hexing,
                (int)eProperty.Focus_Witchcraft,
                (int)eProperty.Focus_Tormentshaper,
                (int)eProperty.Focus_Wraithsight,
                (int)eProperty.Focus_Void_Acolyte,
                (int)eProperty.AllFocusLevels
            };
            return focusBonuses.Contains(bonusType);
        }

        private static double GetUtilityMultiplier(int bonusType)
        {
            if (bonusType < 9 || bonusType == 156) // Stats and Acuity
                return 0.6667;
            else if (bonusType == 145 || bonusType == 148) // MaxSpeed or ArmorFactor
                return 1;
            else if (bonusType == 10 || bonusType == 187 || bonusType == 210 || bonusType == 59) // MaxHealth or similar
                return 0.25;
            else if (bonusType == 9
                || (bonusType >= 11 && bonusType <= 19)
                || bonusType == 71
                || bonusType == 116
                || bonusType == 119
                || bonusType == 146
                || bonusType == 147
                || bonusType == 149
                || (bonusType >= 169 && bonusType <= 172)
                || (bonusType >= 175 && bonusType <= 186)
                || bonusType == 189
                || bonusType == 190
                || (bonusType >= 192 && bonusType <= 196)
                || bonusType == 199
                || (bonusType >= 201 && bonusType <= 209)
                || bonusType == 211
                || (bonusType >= 217 && bonusType <= 220)
                || bonusType == 230
                || bonusType == 231
                || bonusType == 246
                || bonusType == 251
                || bonusType == 252
                || (bonusType >= 256 && bonusType <= 269))
                return 2;
            else if (bonusType == 117
                || (bonusType >= 221 && bonusType <= 229)
                || (bonusType >= 236 && bonusType <= 245))
                return 4;
            else if ((bonusType >= 20 && bonusType <= 58)
                || (bonusType >= 60 && bonusType <= 70)
                || (bonusType >= 72 && bonusType <= 115)
                || bonusType == 118
                || bonusType == 131
                || (bonusType >= 150 && bonusType <= 155)
                || (bonusType >= 163 && bonusType <= 164)
                || (bonusType >= 166 && bonusType <= 168)
                || bonusType == 173
                || bonusType == 174
                || bonusType == 188
                || bonusType == 191
                || bonusType == 197
                || bonusType == 198
                || bonusType == 200
                || (bonusType >= 212 && bonusType <= 217)
                || (bonusType >= 232 && bonusType <= 235)
                || (bonusType >= 247 && bonusType <= 250)
                || (bonusType >= 253 && bonusType <= 255)
                || (bonusType >= 270 && bonusType <= 309))
                return 5;
            else
                return 1; // Default multiplier
        }

        public static int GetGemImbuePoints(int bonusType, int bonusValue)
        {
            if (bonusType == 0 || bonusValue == 0)
                return 0;

            int gemBonus;

            if (bonusType <= (int)eProperty.Stat_Last || bonusType == (int)eProperty.Acuity) // Stat bonuses
                gemBonus = (int)(((bonusValue - 1) * 2 / 3) + 1);
            else if (bonusType == (int)eProperty.MaxMana) // Power cap
                gemBonus = (int)((bonusValue * 2) - 2);
            else if (bonusType == (int)eProperty.MaxHealth) // Hits
                gemBonus = (int)(bonusValue / 4);
            else if (bonusType <= (int)eProperty.Resist_Last) // Resists
                gemBonus = (int)((bonusValue * 2) - 2);
            else if (IsSkillBonus(bonusType)) // Skills
                gemBonus = (int)((bonusValue - 1) * 5);
            else if (bonusType == (int)eProperty.PowerPoolCapBonus) // mana cap
                gemBonus = (int)(((bonusValue * 2) - 2) * 4);
            else if (bonusType == (int)eProperty.MaxHealthCapBonus) // HP cap
                gemBonus = (int)(bonusValue / 4) * 4;
            else if (bonusType >= (int)eProperty.StatCapBonus_First && bonusType <= (int)eProperty.AcuCapBonus) // stat cap
                gemBonus = (int)(((bonusValue - 1) * 2 / 3) + 1) * 4;
            else if (bonusType >= (int)eProperty.MythicalStatCapBonus_First && bonusType <= (int)eProperty.MythicalStatCapBonus_Last) // mythical cap
                gemBonus = (int)(((bonusValue - 1) * 2 / 3) + 1) * 4;
            else if (bonusType >= (int)eProperty.ResCapBonus_First && bonusType <= (int)eProperty.ResCapBonus_Last) // resist cap
                gemBonus = (int)((bonusValue * 2) - 2) * 4;
            else if (IsToABonus(bonusType) || IsOtherSpecialBonus(bonusType))
            {
                double utility = GetUtilityMultiplier(bonusType) * bonusValue;
                gemBonus = (int)Math.Round(utility);
            }
            else if (IsFocusBonus(bonusType))
            {
                gemBonus = 1;
            }
            else
            {
                // Default to 100 for unknown bonuses
                gemBonus = 100;
            }

            if (gemBonus < 1) gemBonus = 1;

            return gemBonus;
        }

        public static int GetItemMaxImbuePoints(ItemTemplate item)
        {
            if (item.level > 51) return 32;
            if (item.level < 1) return 0;
            return itemMaxBonusLevel[item.level - 1, 100 - 94];
        }

        private static readonly int[,] itemMaxBonusLevel =  // taken from mythic Spellcraft calculator
		{
			{0,1,1,1,1,1,1},
			{1,1,1,1,1,2,2},
			{1,1,1,2,2,2,2},
			{1,1,2,2,2,3,3},
			{1,2,2,2,3,3,4},
			{1,2,2,3,3,4,4},
			{2,2,3,3,4,4,5},
			{2,3,3,4,4,5,5},
			{2,3,3,4,5,5,6},
			{2,3,4,4,5,6,7},
			{2,3,4,5,6,6,7},
			{3,4,4,5,6,7,8},
			{3,4,5,6,6,7,9},
			{3,4,5,6,7,8,9},
			{3,4,5,6,7,8,10},
			{3,5,6,7,8,9,10},
			{4,5,6,7,8,10,11},
			{4,5,6,8,9,10,12},
			{4,6,7,8,9,11,12},
			{4,6,7,8,10,11,13},
			{4,6,7,9,10,12,13},
			{5,6,8,9,11,12,14},
			{5,7,8,10,11,13,15},
			{5,7,9,10,12,13,15},
			{5,7,9,10,12,14,16},
			{5,8,9,11,12,14,16},
			{6,8,10,11,13,15,17},
			{6,8,10,12,13,15,18},
			{6,8,10,12,14,16,18},
			{6,9,11,12,14,16,19},
			{6,9,11,13,15,17,20},
			{7,9,11,13,15,17,20},
			{7,10,12,14,16,18,21},
			{7,10,12,14,16,19,21},
			{7,10,12,14,17,19,22},
			{7,10,13,15,17,20,23},
			{8,11,13,15,17,20,23},
			{8,11,13,16,18,21,24},
			{8,11,14,16,18,21,24},
			{8,11,14,16,19,22,25},
			{8,12,14,17,19,22,26},
			{9,12,15,17,20,23,26},
			{9,12,15,18,20,23,27},
			{9,13,15,18,21,24,27},
			{9,13,16,18,21,24,28},
			{9,13,16,19,22,25,29},
			{10,13,16,19,22,25,29},
			{10,14,17,20,23,26,30},
			{10,14,17,20,23,27,31},
			{10,14,17,20,23,27,31},
			{10,15,18,21,24,28,32},
		};
    }
}
