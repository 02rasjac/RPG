using GameDevTV.Inventories;
using RPG.Stats;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Inventories
{
    public class RandomDropper : ItemDropper
    {
        [Tooltip("The radius from drop-point the items can get dropped at")]
        [SerializeField] float scatterDistance = 1f;
        [SerializeField] DropLibrary dropLibrary;

        const int MAX_ATTEMPTS = 30;

        public void RandomDrop()
        {
            int level = GetComponent<BaseStats>().CurrentLevel;
            foreach (var drop in dropLibrary.GetRandomDrops(level))
            {
                DropItem(drop.item, 1);
            }
        }

        protected override Vector3 GetDropLocation()
        {
            for (int i = 0; i < MAX_ATTEMPTS; i++)
            {
                NavMeshHit hit;
                if (NavMesh.SamplePosition(transform.position + Random.insideUnitSphere * scatterDistance, out hit, 0.1f, NavMesh.AllAreas))
                    return hit.position;
            }
            return transform.position;
        }
    }
}