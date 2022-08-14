using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(menuName = "Stats/New progression")]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] progressionCharacterClass;

        Dictionary<CharacterClasses, Dictionary<Stats, float[]>> lookupTable = null;

        /// <summary>
        /// Get health for <paramref name="characterClass"/> based on its <paramref name="level"/>.
        /// </summary>
        /// <param name="stat">The stat you want to get, i.e <c>Stats.Health</c>.</param>
        /// <param name="characterClass">The type of class this character is, i.e <c>CharacterClasses.Player</c>.</param>
        /// <param name="level">The level to get the health for. Requires that <c>level >= 1</c>.</param>
        /// <returns>The health specified for this <paramref name="characterClass"/> and <paramref name="level"/>.</returns>
        public float GetStat(Stats stat, CharacterClasses characterClass, int level)
        {
            BuildLookupTable();

            int index = ClampAndLogLevel() - 1;

            return lookupTable[characterClass][stat][index];

            int ClampAndLogLevel()
            {
                if ((level - 1) < 0)
                {
                    level = 1;
                    Debug.LogError("Level < 1 => index out of range => return stat for level = 1");
                }
                else if (level > lookupTable[characterClass][stat].Length)
                {
                    int oldLevel = level;
                    level = lookupTable[characterClass][stat].Length;
                    Debug.LogError($"No defined stat {stat} for level = {oldLevel} => index out of range => return stat for level = {level}");
                }

                return level;
            }
        }

        /// <summary>
        /// Get the array for <paramref name="stat"/> per level.
        /// </summary>
        /// <param name="stat">The stat you want to get, i.e <c>Stats.ExperienceToLevelUp</c>.</param>
        /// <param name="characterClass">The type of class this character is, i.e <c>CharacterClasses.Player</c>.</param>
        /// <returns>Array of <paramref name="stat"/> per level.</returns>
        public float[] GetLevels(Stats stat, CharacterClasses characterClass)
        {
            BuildLookupTable();
            return lookupTable[characterClass][stat];
        }

        public int MaxLevel(CharacterClasses characterClass)
        {
            BuildLookupTable();
            return lookupTable[characterClass][Stats.ExperienceToLevelUp].Length + 1;
        }

        void BuildLookupTable()
        {
            if (lookupTable != null) return;

            lookupTable = new Dictionary<CharacterClasses, Dictionary<Stats, float[]>>();

            foreach (ProgressionCharacterClass pcc in progressionCharacterClass)
            {
                Dictionary<Stats, float[]> statsDict = new Dictionary<Stats, float[]>();
                foreach (ProgressionStats ps in pcc.stats)
                {
                    statsDict.Add(ps.stat, ps.levels);
                }
                lookupTable.Add(pcc.characterClass, statsDict);
            }
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClasses characterClass;
            public ProgressionStats[] stats;
        }

        [System.Serializable]
        class ProgressionStats
        {
            public Stats stat;
            public float[] levels;
        }
    }
}