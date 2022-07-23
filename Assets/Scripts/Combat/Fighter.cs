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
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float damagePoints = 5f;

        Mover mover;
        ActionScheduler actionScheduler;
        Animator animator;

        Transform target;
        float timeSinceLastAttack;

        void Awake()
        {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

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
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                // Hit()-event will be triggered here.
                timeSinceLastAttack = 0f;
                animator.SetTrigger("attack");
            }
        }

        /// <summary>
        /// Attack-animation event.
        /// </summary>
        void Hit()
        {
            target.GetComponent<Health>().TakeDamage(damagePoints);
        }
    }
}
