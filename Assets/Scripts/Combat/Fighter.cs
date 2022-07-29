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
        [Tooltip("Where the weapon is position, i.e under right hand.")]
        [SerializeField] Transform handTransform = null;
        [SerializeField] Weapon weapon = null;

        Mover mover;
        ActionScheduler actionScheduler;
        Animator animator;

        Health target;
        float timeSinceLastAttack = Mathf.Infinity;

        void Awake()
        {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
            SpawnWeapon();
        }

        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target != null)
            {
                if (weapon.Range <= Vector3.Distance(transform.position, target.transform.position))
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

            if (timeSinceLastAttack > weapon.TimeBetweenAttacks)
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

        void SpawnWeapon()
        {
            if (weapon == null) return;
            
            weapon.Spawn(handTransform, animator);
        }

        /// <summary>
        /// Attack-animation event.
        /// </summary>
        void Hit()
        {
            if (target == null) return;
            target.TakeDamage(weapon.Damage);
        }
    }
}
