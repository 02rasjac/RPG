using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneIndex = -1;
        [SerializeField] Transform spawnPoint;

        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            StartCoroutine(Transition());
        }

        IEnumerator Transition()
        {
            int oldSceneIndex = SceneManager.GetActiveScene().buildIndex;
            DontDestroyOnLoad(this);
            yield return SceneManager.LoadSceneAsync(sceneIndex);

            Portal otherPortal = GetOtherPortal(oldSceneIndex);
            UpdatePlayer(otherPortal);

            Destroy(gameObject);
        }

        Portal GetOtherPortal(int oldIndex)
        {
            foreach (Portal portal in FindObjectsOfType<Portal>()) {
                if (portal.sceneIndex == oldIndex) return portal;
            }
            return null;
        }

        void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            // Make sure character faces correct direcition. (Thanks Corey)
            Vector3 directionVector = (otherPortal.spawnPoint.position - otherPortal.transform.position).normalized;
            player.transform.rotation = Quaternion.LookRotation(directionVector);
        }
    }
}