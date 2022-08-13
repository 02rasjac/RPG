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

        void Update()
        {
            if (gameObject.CompareTag("Player")) print(GetLevel());
        }

        public float GetStat(Stats stat) => progression.GetStat(stat, characterClass, currentLevel);

        /// <summary>
        /// Check if experience-holder can level up and return it's current level AFTER potential levelup.
        /// </summary>
        /// <returns>Current level or it's new level.</returns>
        public int GetLevel()
        {
            Experience experience = GetComponent<Experience>();
            if (experience == null) return startLevel;

            if (currentLevel >= progression.MaxLevel(characterClass)) return currentLevel;

            float experienceToLevelUp = GetStat(Stats.ExperienceToLevelUp);
            if (experience.GetExperience() >= experienceToLevelUp)
            {
                currentLevel++;
            }

            return currentLevel;
        }
    }
}