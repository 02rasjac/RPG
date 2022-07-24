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
        [SerializeField] float suspicionTime = 2f;

        Fighter fighter;
        Health health;
        Mover mover;
        ActionScheduler actionScheduler;

        GameObject player;

        Vector3 guardLocation;
        float timeSinceLastSawPlayer = Mathf.Infinity;

        void Awake()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
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
                    timeSinceLastSawPlayer = 0f;
                    fighter.Attack(player);
                }
                else if (timeSinceLastSawPlayer < suspicionTime)
                {
                    actionScheduler.CancelCurrentAction();
                }
                else
                {
                    mover.StartMoveAction(guardLocation);
                }

                timeSinceLastSawPlayer += Time.deltaTime;
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}