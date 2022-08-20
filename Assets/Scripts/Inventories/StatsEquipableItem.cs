using GameDevTV.Inventories;
using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Inventories
{
    [CreateAssetMenu(menuName = "RPG/Inventory/Equipable Item")]
    public class StatsEquipableItem : EquipableItem, IModifierProvider
    {
        [SerializeField] Modifier[] additiveModifiers;
        [SerializeField] Modifier[] multiplyingModifiers;

        public IEnumerable<float> GetAdditiveModifiers(Stats.Stats stat)
        {
            foreach (var item in additiveModifiers)
            {
                if (item.stat == stat) yield return item.value;
            }
        }

        public IEnumerable<float> GetMultiplyingModifiers(Stats.Stats stat)
        {
            foreach (var item in multiplyingModifiers)
            {
                if (item.stat == stat) yield return item.value;
            }
        }

        [System.Serializable]
        struct Modifier
        {
            public Stats.Stats stat;
            public float value;
        }
    }
}