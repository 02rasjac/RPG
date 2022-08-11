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
        /// <param name="characterClass">The type of class this character is, i.e <c>CharacterClasses.Player</c>.</param>
        /// <param name="level">The level to get the health for. Requires that <c>level >= 1</c>.</param>
        /// <returns>The health specified for this <paramref name="characterClass"/> and <paramref name="level"/>.</returns>
        public float GetHealth(CharacterClasses characterClass, int level)
        {
            //var pcc = FindProgressionCharacterClass(characterClass);
            //if ((level - 1) < 0)
            //{
            //    level = 1;
            //    Debug.LogError("Level < 1 => index out of range => sets healht for level = 1");
            //}
            //else if (level > pcc.health.Length)
            //{
            //    level = pcc.health.Length;
            //    Debug.LogError("No defined health for this level => index out of range => sets health for level = " + level);
            //}

            //return pcc.health[level - 1];
            return 10f;
        }

        public float GetExperienceReward(CharacterClasses characterClass, int level)
        {
            return 10;
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
        }

        [System.Serializable]
        class ProgressionStats
        {
            public Stats stat;
            public float[] levels;
        }
    }
}