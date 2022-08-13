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

        int currentLevel;

        void Awake()
        {
            currentLevel = startLevel;
        }

        public float GetStat(Stats stat) => progression.GetStat(stat, characterClass, currentLevel);

        /// <summary>
        /// Potentially level up and return it's current level AFTER potential levelup.
        /// </summary>
        /// <returns>Current level or it's new level.</returns>
        public int UpdateLevel(float xp)
        {
            if (currentLevel >= progression.MaxLevel(characterClass)) return currentLevel;

            float experienceToLevelUp = GetStat(Stats.ExperienceToLevelUp);
            if (xp >= experienceToLevelUp)
            {
                currentLevel++;
            }

            return currentLevel;
        }
    }
}