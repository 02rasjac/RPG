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

        public float GetStat(Stats stat, int level) => progression.GetStat(stat, characterClass, level);

        /// <summary>
        /// Potentially level up and return it's current level AFTER potential levelup.
        /// </summary>
        /// <returns>Current level or it's new level.</returns>
        public void UpdateLevel()
        {
            if (currentLevel < 1) currentLevel = 1; // Initialise level
            if (experience == null) return;
            if (currentLevel >= progression.MaxLevel(characterClass)) return;

            float experienceToLevelUp = GetStat(Stats.ExperienceToLevelUp);
            if (experience.GetExperience() >= experienceToLevelUp)
                LevelUp();
        }

        void LevelUp()
        {
            currentLevel++;
            OnLevelUp(currentLevel - 1);
            GameObject levelUpObj = Instantiate(levelUpPrefab, transform);
            Destroy(levelUpObj, 10f);
        }
    }
}