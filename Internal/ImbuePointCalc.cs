namespace AmteCreator.Internal
{
    public static class ImbuePointCalc
    {
        public static int GetGemImbuePoints(int bonusType, int bonusValue)
        {
            if (bonusType == 0 || bonusValue == 0)
                return 0;
            int gemBonus;
            if (bonusType <= (int)eProperty.Stat_Last || bonusType == (int)eProperty.Acuity) //stat
                gemBonus = ((bonusValue - 1) * 2 / 3) + 1;
            else if (bonusType == (int)eProperty.MaxMana) //mana
                gemBonus = (bonusValue * 2) - 2;
            else if (bonusType == (int)eProperty.MaxHealth) //HP
                gemBonus = bonusValue / 4;
            else if (bonusType <= (int)eProperty.Resist_Last) //resist
                gemBonus = (bonusValue * 2) - 2;
            else if (bonusType <= (int)eProperty.Skill_Last) //skill
                gemBonus = (bonusValue - 1) * 5;
            else if (bonusType >= (int)eProperty.ToABonus_First && bonusType <= (int)eProperty.ToABonus_Last) // Toa
                gemBonus = bonusValue * 100;
            else if (bonusType == (int)eProperty.PowerPoolCapBonus) //mana cap
                gemBonus = ((bonusValue * 2) - 2) * 4;
            else if (bonusType == (int)eProperty.MaxHealthCapBonus) //HP cap
                gemBonus = bonusValue / 4 * 4;
            else if (bonusType >= (int)eProperty.StatCapBonus_First && bonusType <= (int)eProperty.StatCapBonus_Last) //stat cap
                gemBonus = (((bonusValue - 1) * 2 / 3) + 1) * 4;
            else if (bonusType >= (int)eProperty.ResCapBonus_First && bonusType <= (int)eProperty.ResCapBonus_Last) //resist cap
                gemBonus = ((bonusValue * 2) - 2) * 4;
            else if (bonusType == (int)eProperty.MaxSpeed ||
                bonusType == (int)eProperty.MaxConcentration ||
                bonusType == (int)eProperty.ArmorFactor ||
                bonusType == (int)eProperty.ArmorAbsorption ||
                bonusType == (int)eProperty.HealthRegenerationRate ||
                bonusType == (int)eProperty.PowerRegenerationRate ||
                bonusType == (int)eProperty.EnduranceRegenerationRate ||
                bonusType == (int)eProperty.SpellRange ||
                bonusType == (int)eProperty.ArcheryRange ||
                bonusType == (int)eProperty.MeleeSpeed ||
                bonusType == (int)eProperty.LivingEffectiveLevel ||
                bonusType == (int)eProperty.EvadeChance ||
                bonusType == (int)eProperty.BlockChance ||
                bonusType == (int)eProperty.ParryChance ||
                bonusType == (int)eProperty.FatigueConsumption ||
                bonusType == (int)eProperty.MeleeDamage ||
                bonusType == (int)eProperty.RangedDamage ||
                bonusType == (int)eProperty.FumbleChance ||
                bonusType == (int)eProperty.MesmerizeDuration ||
                bonusType == (int)eProperty.StunDuration ||
                bonusType == (int)eProperty.SpeedDecreaseDuration ||
                bonusType == (int)eProperty.BladeturnReinforcement ||
                bonusType == (int)eProperty.DefensiveBonus ||
                bonusType == (int)eProperty.SpellFumbleChance ||
                bonusType == (int)eProperty.NegativeReduction ||
                bonusType == (int)eProperty.PieceAblative ||
                bonusType == (int)eProperty.ReactionaryStyleDamage ||
                bonusType == (int)eProperty.SpellPowerCost ||
                bonusType == (int)eProperty.StyleCostReduction ||
                bonusType == (int)eProperty.ToHitBonus ||
                bonusType == (int)eProperty.AllSkills ||
                bonusType == (int)eProperty.WeaponSkill ||
                bonusType == (int)eProperty.CriticalMeleeHitChance ||
                bonusType == (int)eProperty.CriticalArcheryHitChance ||
                bonusType == (int)eProperty.CriticalSpellHitChance ||
                bonusType == (int)eProperty.WaterSpeed ||
                bonusType == (int)eProperty.SpellLevel ||
                bonusType == (int)eProperty.MissHit ||
                bonusType == (int)eProperty.KeepDamage ||
                bonusType == (int)eProperty.DPS ||
                bonusType == (int)eProperty.MagicAbsorption ||
                bonusType == (int)eProperty.CriticalHealHitChance ||
                bonusType == (int)eProperty.BountyPoints ||
                bonusType == (int)eProperty.XpPoints ||
                bonusType == (int)eProperty.Resist_Natural ||
                bonusType == (int)eProperty.ExtraHP ||
                bonusType == (int)eProperty.Conversion ||
                bonusType == (int)eProperty.StyleAbsorb ||
                bonusType == (int)eProperty.RealmPoints ||
                bonusType == (int)eProperty.ArcaneSyphon ||
                bonusType == (int)eProperty.MaxProperty)
                gemBonus = 1000000;
            else
                gemBonus = 1;// focus


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
