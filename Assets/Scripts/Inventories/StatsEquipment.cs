using GameDevTV.Inventories;
using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Inventories
{
    public class StatsEquipment : Equipment, IModifierProvider
    {
        public IEnumerable<float> GetAdditiveModifiers(Stats.Stats stat)
        {
            foreach (var slot in GetAllPopulatedSlots())
            {
                var item = GetItemInSlot(slot) as IModifierProvider;
                if (item == null) continue;
                foreach (var mod in item.GetAdditiveModifiers(stat))
                {
                    yield return mod;
                }
            }
        }

        public IEnumerable<float> GetMultiplyingModifiers(Stats.Stats stat)
        {
            foreach (var slot in GetAllPopulatedSlots())
            {
                var item = GetItemInSlot(slot) as IModifierProvider;
                if (item == null) continue;
                foreach (var mod in item.GetMultiplyingModifiers(stat))
                {
                    yield return mod;
                }
            }
        }
    }
}