using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Core;
using RPG.Movement;

namespace RPG.Combat
{
    [RequireComponent(typeof(ActionScheduler))]
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float damagePoints = 5f;

        Mover mover;
        ActionScheduler actionScheduler;
        Animator animator;

        Health target;
        float timeSinceLastAttack;

        void Awake()
        {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
            timeSinceLastAttack = timeBetweenAttacks;
        }

        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target != null)
            {
                if (weaponRange <= Vector3.Distance(transform.position, target.transform.position))
                {
                    mover.SetDestination(target.transform.position);
                }
                else
                {
                    mover.Stop();
                    AttackBehavior();
                }
            }
        }

        public void Attack(GameObject target)
        {
            animator.ResetTrigger("stopAttack");
            actionScheduler.StartAction(this);
            this.target = target.GetComponent<Health>();
        }

        /// <summary>
        /// Test if <paramref name="testTarget"/> is attackable.
        /// </summary>
        /// <param name="testTarget">Target to test.</param>
        /// <returns><c>true</c> if <paramref name="testTarget"/> is not null and is alive.</returns>
        public bool CanAttack(GameObject testTarget)
        {
            //return testTarget != null && !testTarget.GetComponent<Health>().IsDead;
            return !testTarget.GetComponent<Health>().IsDead;
        }

        public void Cancel()
        {
            mover.Stop();
            target = null;
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
        }

        void AttackBehavior()
        {
            transform.LookAt(target.transform);

            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                // Hit()-event will be triggered here.
                timeSinceLastAttack = 0f;
                if (target.IsDead)
                {
                    Cancel();
                    return;
                }
                animator.SetTrigger("attack");
            }
        }

        /// <summary>
        /// Attack-animation event.
        /// </summary>
        void Hit()
        {
            if (target == null) return;
            target.TakeDamage(damagePoints);
        }
    }
}
