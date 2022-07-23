using System.Collections;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        GameObject player;

        // Use this for initialization
        void Start()
        {
            player = GameObject.FindWithTag("Player");
        }

        // Update is called once per frame
        void Update()
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= chaseDistance)
            {
                print($"{name} should chase {player.name}");
            }
        }
    }
}