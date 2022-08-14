using System;
using System.Collections;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startLevel = 1;
        [Range(0f, 100f)]
        [SerializeField] public float levelUpHealPercentage = 70f;
        [SerializeField] CharacterClasses characterClass;
        [SerializeField] Progression progression;
        [SerializeField] GameObject levelUpPrefab;

        int currentLevel = 0;
        public int CurrentLevel { get { return currentLevel; } }

        Experience experience;

        public delegate void OnLevelUpDel(int oldLevel);
        public event OnLevelUpDel OnLevelUp;

        void Start()
        {
            experience = GetComponent<Experience>();
            UpdateLevel();
            if (experience != null)
            {
                experience.ExperienceChanged += UpdateLevel;
            }
        }

        public float GetStat(Stats stat) => GetStat(stat, currentLevel);

        public float GetStat(Stats stat, int level)
        {
            float baseStat = progression.GetStat(stat, characterClass, level);
            float additive = GetAdditiveModifiers(stat);
            return baseStat + additive;
        }

        /// <summary>
        /// Potentially level up (if enough XP & within available range).
        /// </summary>
        public void UpdateLevel()
        {
            if (currentLevel < 1) currentLevel = 1; // Initialise level
            if (experience == null) return;
            if (currentLevel >= progression.MaxLevel(characterClass)) return;

            float experienceToLevelUp = GetStat(Stats.ExperienceToLevelUp);
            if (experience.GetExperience() >= experienceToLevelUp)
                LevelUp();
        }

        /// <summary>
        /// Calculate the sum of additive modifieres (i.e weapons can add onto base damage).
        /// </summary>
        /// <param name="stat">The stat to calculate modifiers for.</param>
        /// <returns>The total sum from the modifiers.</returns>
        float GetAdditiveModifiers(Stats stat)
        {
            float sum = 0f;
            var providers = GetComponents<IModifierProvider>();
            foreach (IModifierProvider provider in providers)
            {
                foreach (float modifier in provider.GetAdditiveModifier(stat))
                {
                    sum += modifier;
                }
            }

            return sum;
        }

        void LevelUp()
        {
            currentLevel++;
            if (OnLevelUp != null) OnLevelUp(currentLevel - 1);
            GameObject levelUpObj = Instantiate(levelUpPrefab, transform);
            Destroy(levelUpObj, 10f);
        }
    }
}