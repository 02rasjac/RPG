using System.Collections;
using UnityEngine;

using RPG.Core;
using RPG.Combat;
using RPG.Movement;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        Fighter fighter;
        Health health;
        Mover mover;

        GameObject player;

        Vector3 guardLocation;

        void Awake()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
        }

        // Use this for initialization
        void Start()
        {
            player = GameObject.FindWithTag("Player");
            guardLocation = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (!health.IsDead)
            {
                float dist = Vector3.Distance(transform.position, player.transform.position);
                if (dist <= chaseDistance && fighter.CanAttack(player))
                {
                    fighter.Attack(player);
                }
                else
                {
                    mover.StartMoveAction(guardLocation);
                }

            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}