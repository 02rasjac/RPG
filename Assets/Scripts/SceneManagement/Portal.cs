using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneIndexToBuild = -1;

        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            StartCoroutine(Transition());
        }

        IEnumerator Transition()
        {
            DontDestroyOnLoad(this);
            yield return SceneManager.LoadSceneAsync(sceneIndexToBuild);
            print("Scene loaded");
            Destroy(gameObject);
        }
    }
}