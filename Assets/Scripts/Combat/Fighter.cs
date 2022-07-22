using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Core;
using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;

        Mover mover;
        ActionScheduler actionScheduler;
        Animator animator;

        Transform target;

        void Awake()
        {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (target != null)
            {
                if (weaponRange <= Vector3.Distance(transform.position, target.position))
                {
                    mover.SetDestination(target.position);
                }
                else
                {
                    mover.Stop();
                    AttackBehavior();
                }
            }
        }

        public void Attack(CombatTarget target)
        {
            actionScheduler.StartAction(this);
            this.target = target.transform;
        }

        public void Cancel()
        {
            target = null;
        }

        void AttackBehavior()
        {
            animator.SetTrigger("attack");
        }

        /// <summary>
        /// Attack-animation event.
        /// </summary>
        void Hit()
        {

        }
    }
}
