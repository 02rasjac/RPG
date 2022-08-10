using System.Collections;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(menuName = "Stats/New progression")]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] progressionCharacterClass;

        [System.Serializable]
        class ProgressionCharacterClass
        {
            [SerializeField] CharacterClasses characterClass;
            [SerializeField] float[] health;
        }
    }
}