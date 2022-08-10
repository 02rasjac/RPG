﻿using System.Collections;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startLevel = 1;
        [SerializeField] CharacterClasses characterClass;
        [SerializeField] Progression progression;

        public float GetHealth()
        {
            return progression.GetHealth(characterClass, startLevel);
        }
    }
}