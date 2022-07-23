using System.Collections;
using UnityEngine;

using RPG.Combat;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        Fighter fighter;

        GameObject player;

        void Awake()
        {
            fighter = GetComponent<Fighter>();
        }

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
                if (fighter.CanAttack(player))
                {
                    fighter.Attack(player);
                }
            }
            else
            {
                fighter.Cancel();
            }
        }
    }
}