using GameDevTV.Inventories;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Inventories
{
    [CreateAssetMenu(menuName = "RPG/Inventory/Drop Library")]
    public class DropLibrary : ScriptableObject
    {
        [Tooltip("Array of items that can be dropped.")]
        [SerializeField] DropConfig[] potentialDrops;
        [Tooltip("Chance of dropping anything.")]
        [SerializeField] float[] dropChancePercentage;
        [Tooltip("Min- and max number of different items that can be dropped.")]
        [SerializeField] Vector2Int[] minMaxDrops;

        public IEnumerable<Dropped> GetRandomDrops(int level)
        {
            if (!ShouldRandomDrop(level)) yield break;
            for (int i = 0; i < GetRandomNumberOfDrops(level); i++)
            {
                yield return GetRandomDrop(level);
            }
        }

        bool ShouldRandomDrop(int level)
        {
            float roll = Random.Range(0f, 100f);
            return roll <= GetByLevel(dropChancePercentage, level);
        }

        int GetRandomNumberOfDrops(int level)
        {
            Vector2Int minMaxForLevel = GetByLevel(minMaxDrops, level);
            return Random.Range(minMaxForLevel.x, minMaxForLevel.y + 1);
        }

        Dropped GetRandomDrop(int level)
        {
            DropConfig dropConfig = SelectRandomItem(level);
            Dropped drop = new Dropped();
            drop.item = dropConfig.item;
            drop.number = dropConfig.GetRandomNumber(level);
            return drop;
        }

        DropConfig SelectRandomItem(int level)
        {
            float randomRoll = Random.Range(0, GetTotalChance(level));
            float chanceSum = 0f;
            foreach (var drop in potentialDrops)
            {
                chanceSum += GetByLevel(drop.relativeChance, level);
                if (chanceSum > randomRoll) return drop;
            }
            return null;
        }

        float GetTotalChance(int level)
        {
            float total = 0f;
            foreach (var drop in potentialDrops)
            {
                total += GetByLevel(drop.relativeChance, level);
            }
            return total;
        }

        public struct Dropped
        {
            public InventoryItem item;
            public int number;
        }

        [System.Serializable]
        class DropConfig
        {
            [Tooltip("The inventory item to be dropped.")]
            public InventoryItem item;
            [Tooltip("Change of dropping this item RELATIVE to oter items that can be dropped.")]
            public float[] relativeChance;
            [Tooltip("Stack size of the dropped item. Non-stackable items should be (1,1).")]
            public Vector2Int[] minMaxNumber;

            public int GetRandomNumber(int level)
            {
                Vector2Int minMaxForLevel = GetByLevel(minMaxNumber, level);
                return item.IsStackable() ? Random.Range(minMaxForLevel.x, minMaxForLevel.y + 1) : 1;
            }
        }

        static T GetByLevel<T>(T[] array, int level)
        {
            if (array.Length == 0) return default(T);
            if (level < 1) return array[0];
            if (level > array.Length) return array[array.Length - 1];
            return array[level - 1];
        }
    }
}