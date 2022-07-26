using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B,C,D,E,F,G
        }

        [SerializeField] DestinationIdentifier destinationIdentifier;
        [SerializeField] int sceneIndex = -1;
        [SerializeField] Transform spawnPoint;

        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            StartCoroutine(Transition());
        }

        IEnumerator Transition()
        {
            if (sceneIndex < 0)
            {
                Debug.LogError("No sceneIndex set (sceneIndex < 0)");
                yield break;
            }

            DontDestroyOnLoad(this);
            yield return SceneManager.LoadSceneAsync(sceneIndex);

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            Destroy(gameObject);
        }

        Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>()) {
                if (portal.destinationIdentifier == destinationIdentifier && portal != this)
                    return portal;
            }
            print(SceneManager.GetActiveScene().buildIndex);
            return null;
        }

        void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            var nma = player.GetComponent<NavMeshAgent>();
            nma.enabled = false;
            player.transform.position = otherPortal.spawnPoint.position;
            print(player.transform.position);
            // Make sure character faces correct direcition. (Thanks Corey)
            Vector3 directionVector = (otherPortal.spawnPoint.position - otherPortal.transform.position).normalized;
            player.transform.rotation = Quaternion.LookRotation(directionVector);
            nma.enabled = true;
        }
    }
}