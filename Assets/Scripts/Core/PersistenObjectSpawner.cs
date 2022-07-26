using System.Collections;
using UnityEngine;

namespace RPG.Core
{
    public class PersistenObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab;

        static bool hasSpawned = false;

        void Awake()
        {
            if (hasSpawned) return;

            hasSpawned = true;
            SpawnObject();
        }

        void SpawnObject()
        {
            GameObject obj = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(obj);
        }
    }
}