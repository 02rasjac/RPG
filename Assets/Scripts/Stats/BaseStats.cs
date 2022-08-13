using System.Collections;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startLevel = 1;
        [SerializeField] CharacterClasses characterClass;
        [SerializeField] Progression progression;

        int currentLevel = 0;
        public int CurrentLevel { get { return currentLevel; } }

        Experience experience;

        void Start()
        {
            experience = GetComponent<Experience>();
            UpdateLevel();
            if (experience != null)
            {
                experience.ExperienceChanged += UpdateLevel;
            }
        }

        public float GetStat(Stats stat) => progression.GetStat(stat, characterClass, currentLevel);

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
            {
                currentLevel++;
            }
        }
    }
}