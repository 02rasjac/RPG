using System.Collections;
using UnityEngine;

namespace RPG.Core
{
    public class PersistenObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject[] persistentObjectPrefabs;

        static bool hasSpawned = false;

        void Awake()
        {
            if (hasSpawned) return;

            hasSpawned = true;
            SpawnObjects();
        }

        void SpawnObjects()
        {
            foreach (var prefab in persistentObjectPrefabs)
            {
                GameObject obj = Instantiate(prefab);
                DontDestroyOnLoad(obj);
            }
        }
    }
}