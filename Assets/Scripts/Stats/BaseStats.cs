using System;
using System.Collections;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
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

        void Awake()
        {
            experience = GetComponent<Experience>();
        }

        void Start()
        {
            UpdateLevel();
        }

        void OnEnable()
        {
            if (experience != null)
            {
                experience.ExperienceChanged += UpdateLevel;
            }
        }

        void OnDisable()
        {
            if (experience != null)
            {
                experience.ExperienceChanged -= UpdateLevel;
            }
        }

        /// <summary>
        /// Get the modified stat. Formula used is <c>(baseStat + additive) * multiply</c>.
        /// </summary>
        /// <param name="stat">The stat to fetch.</param>
        /// <returns>Modified stat.</returns>
        public float GetStat(Stats stat)
        {
            float baseStat = GetBaseStat(stat);
            float additive = GetAdditiveModifier(stat);
            float multiply = GetMultiplyingModifier(stat);
            return (baseStat + additive) * multiply;
        }

        /// <summary>
        /// Get the unmodified base stat.
        /// </summary>
        /// <param name="stat">The stat to fetch.</param>
        /// <returns>The stat value.</returns>
        public float GetBaseStat(Stats stat) => progression.GetStat(stat, characterClass, currentLevel);

        /// <summary>
        /// Potentially level up (if enough XP & within available range).
        /// </summary>
        public void UpdateLevel()
        {
            if (currentLevel < 1) currentLevel = 1; // Initialise level
            if (experience == null) return;
            if (currentLevel >= progression.MaxLevel(characterClass)) return;

            float experienceToLevelUp = GetBaseStat(Stats.ExperienceToLevelUp);
            if (experience.GetExperience() >= experienceToLevelUp)
                LevelUp();

            void LevelUp()
            {
                currentLevel++;
                if (OnLevelUp != null) OnLevelUp(currentLevel - 1);
                GameObject levelUpObj = Instantiate(levelUpPrefab, transform);
                Destroy(levelUpObj, 10f);
            }
        }

        /// <summary>
        /// Calculate the sum of additive modifieres (i.e weapons can add onto base damage).
        /// </summary>
        /// <param name="stat">The stat to calculate modifiers for.</param>
        /// <returns>The total sum from the modifiers.</returns>
        float GetAdditiveModifier(Stats stat)
        {
            float sum = 0f;
            var providers = GetComponents<IModifierProvider>();
            foreach (IModifierProvider provider in providers)
            {
                foreach (float modifier in provider.GetAdditiveModifiers(stat))
                {
                    sum += modifier;
                }
            }

            return sum;
        }

        /// <summary>
        /// Calculate the factor of all multiplying modifieres (i.e weapons can multiply the damage AFTER additive modifiers).
        /// </summary>
        /// <example>
        /// If two multipliers are x = 1.2 (+20%) and y = 0.95 (-5%), then the return value is 
        /// x * y = 1.2 * 0.95 = 1.14. That means the total increasing percentage is +14% of the (base damage + additive value).
        /// </example>
        /// <param name="stat">The stat to calculate modifiers for.</param>
        /// <returns>The total factor from the modifiers.</returns>
        float GetMultiplyingModifier(Stats stat)
        {
            float factor = 1f; // Don't initialize as 0 since all multiplications would result in factor still being 0...
            var providers = GetComponents<IModifierProvider>();
            foreach (IModifierProvider provider in providers)
            {
                foreach (float modifier in provider.GetMultiplyingModifiers(stat))
                {
                    factor *= modifier;
                }
            }

            return factor;
        }
    }
}