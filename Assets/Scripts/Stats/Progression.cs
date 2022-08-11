using System.Collections;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(menuName = "Stats/New progression")]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] progressionCharacterClass;

        /// <summary>
        /// Get health for <paramref name="characterClass"/> based on its <paramref name="level"/>.
        /// </summary>
        /// <param name="stat">The stat you want to get, i.e <c>Stats.Health</c>.</param>
        /// <param name="characterClass">The type of class this character is, i.e <c>CharacterClasses.Player</c>.</param>
        /// <param name="level">The level to get the health for. Requires that <c>level >= 1</c>.</param>
        /// <returns>The health specified for this <paramref name="characterClass"/> and <paramref name="level"/>.</returns>
        public float GetStat(Stats stat, CharacterClasses characterClass, int level)
        {
            ProgressionCharacterClass pcc = FindProgressionCharacterClass(characterClass);
            ProgressionStats ps = pcc.FindProgressionStat(stat);
            if (ps == null)
            {
                Debug.LogError($"Requested stat {stat} does not exist in character class {characterClass}.");
                return -1f;
            }
            if ((level - 1) < 0)
            {
                level = 1;
                Debug.LogError("Level < 1 => index out of range => return stat for level = 1");
            }
            else if (level > ps.levels.Length)
            {
                level = ps.levels.Length;
                Debug.LogError("No defined health for this level => index out of range => return stat for level = " + level);
            }

            return ps.levels[level - 1];
        }

        ProgressionCharacterClass FindProgressionCharacterClass(CharacterClasses characterClass)
        {
            foreach (var item in progressionCharacterClass)
            {
                if (item.characterClass == characterClass) return item;
            }
            return null;
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClasses characterClass;
            public ProgressionStats[] stats;

            public ProgressionStats FindProgressionStat(Stats stat)
            {
                foreach (var item in stats)
                {
                    if (item.stat == stat) return item;
                }
                return null;
            }
        }

        [System.Serializable]
        class ProgressionStats
        {
            public Stats stat;
            public float[] levels;
        }
    }
}