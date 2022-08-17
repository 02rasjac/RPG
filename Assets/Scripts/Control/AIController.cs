using System.Collections;
using UnityEngine;

using RPG.Attributes;
using RPG.Core;
using RPG.Combat;
using RPG.Movement;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [Header("Gameplay mechanics")]
        [SerializeField] float patrolSpeed = 1.558401f;
        [SerializeField] float fightSpeed = 3.5f;
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 2f;
        [SerializeField] float aggroTime = 2f;

        [Header("Patrolling")]
        [SerializeField] PatrolPath patrolPath;
        [Tooltip("Maximum distance to waypoint befor going to next.")]
        [SerializeField] float waypointTolerance = 1f;
        [Tooltip("Time to wait at each checkpoint.")]
        [SerializeField] float waitTime = 1f;

        Fighter fighter;
        Health health;
        Mover mover;
        ActionScheduler actionScheduler;

        GameObject player;

        // States/memory
        Vector3 guardLocation;
        int currentWaypointIndex = 0;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceAggrevated = Mathf.Infinity;
        float timeSinceLastMove = 0f;

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

            if (patrolPath == null)
            {
                guardLocation = transform.position;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!health.IsDead)
            {
                if (IsAggrevated() && fighter.CanAttack(player))
                {
                    timeSinceLastSawPlayer = 0f;
                    AttackBehaviour();
                }
                else if (timeSinceLastSawPlayer < suspicionTime)
                {
                    SuspectBehaviour();
                }
                else
                {
                    PatrolBehaviour();
                }

                timeSinceLastSawPlayer += Time.deltaTime;
            }
        }

        public void Aggrevate()
        {
            timeSinceAggrevated = 0;
        }

        bool IsAggrevated()
        {
            float dist = Vector3.Distance(transform.position, player.transform.position);
            bool inRange = dist <= chaseDistance || dist < fighter.CurrentWeaponConfig.Range;
            bool isAggro = timeSinceAggrevated < aggroTime;
            return inRange || isAggro;
        }

        void PatrolBehaviour()
        {
            // No patrolpath, proceed with normal guarding
            if (patrolPath == null)
            {
                mover.StartMoveAction(guardLocation);
                return;
            }

            if (AtWaypoint() && (timeSinceLastMove += Time.deltaTime) > waitTime)
            {
                timeSinceLastMove = 0;
                CycleWaypoint();
            }

            mover.SetSpeed(patrolSpeed);
            mover.StartMoveAction(GetWaypoint());
        }

        bool AtWaypoint()
        {
            float distToWaypoint = Vector3.Distance(GetWaypoint(), transform.position);
            return distToWaypoint < waypointTolerance;
        }

        void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        Vector3 GetWaypoint()
        {
            return patrolPath.GetChildPosition(currentWaypointIndex);
        }

        void SuspectBehaviour()
        {
            actionScheduler.CancelCurrentAction();
        }

        void AttackBehaviour()
        {
            mover.SetSpeed(fightSpeed);
            fighter.Attack(player);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}