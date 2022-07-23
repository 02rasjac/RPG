using System.Collections;
using UnityEngine;

using RPG.Core;
using RPG.Combat;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        Fighter fighter;
        Health health;

        GameObject player;

        void Awake()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
        }

        // Use this for initialization
        void Start()
        {
            player = GameObject.FindWithTag("Player");
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
                    fighter.Cancel();
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