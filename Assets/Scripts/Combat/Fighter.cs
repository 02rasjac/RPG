using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Attributes;
using RPG.Core;
using RPG.Movement;
using RPG.Saving;
using RPG.Stats;
using Newtonsoft.Json.Linq;

namespace RPG.Combat
{
    [RequireComponent(typeof(ActionScheduler))]
    public class Fighter : MonoBehaviour, IAction, ISaveable, IModifierProvider
    {
        [Tooltip("Where the weapon is position, i.e under right hand.")]
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] WeaponConfig defaultWeaponConfig = null;
        //[Tooltip("The name of the weapon scriptable object.")]
        //[SerializeField] string defaultWeaponName = "Unarmed";

        Mover mover;
        ActionScheduler actionScheduler;
        Animator animator;
        BaseStats baseStats;

        Health target;
        public Health Target { get { return target; } }
        WeaponConfig currentWeaponConfig;
        public WeaponConfig CurrentWeaponConfig { get { return currentWeaponConfig; } }
        Weapon currentWeapon;
        float timeSinceLastAttack = Mathf.Infinity;

        void Awake()
        {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
            baseStats = GetComponent<BaseStats>();
            //EquipWeaponFromName(defaultWeaponName);
            currentWeaponConfig = defaultWeaponConfig;
        }

        void Start()
        {
            EquipWeapon(currentWeaponConfig);
        }

        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target != null)
            {
                if (currentWeaponConfig.Range <= Vector3.Distance(transform.position, target.transform.position))
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
            return !testTarget.GetComponent<Health>().IsDead
                && mover.CanMoveTo(testTarget.transform.position);

        }

        public void Cancel()
        {
            mover.Stop();
            target = null;
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
        }

        /// <summary>
        /// Equip <paramref name="weaponConfig"/> as the current weapon.
        /// </summary>
        /// <param name="weaponConfig">The weapon to equip.</param>
        public void EquipWeapon(WeaponConfig weaponConfig)
        {
            currentWeaponConfig = weaponConfig;
            currentWeapon = currentWeaponConfig.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        void EquipWeaponFromName(string name)
        {
            //! USING RESOURCES IS BAD
            WeaponConfig weaponConfig = Resources.Load<WeaponConfig>(name);
            if (weaponConfig == null) weaponConfig = defaultWeaponConfig;
            EquipWeapon(weaponConfig);
        }

        void AttackBehavior()
        {
            transform.LookAt(target.transform);

            if (timeSinceLastAttack > currentWeaponConfig.TimeBetweenAttacks)
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

            float damage = baseStats.GetStat(Stats.Stats.Damage);
            currentWeapon?.OnHit();
            if (currentWeaponConfig.HasProjectile())
            {
                currentWeaponConfig.LaunchProjectile(rightHandTransform, leftHandTransform, target, gameObject, damage);
            }
            else
            {
                target.TakeDamage(gameObject, damage);
            }
        }

        /// <summary>
        /// Shoot-animation event.
        /// </summary>
        void Shoot()
        {
            Hit();
        }

        void OnDrawGizmosSelected()
        {
            // Render attack-range
            if (defaultWeaponConfig != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, defaultWeaponConfig.Range);
            }
        }

        public JToken CaptureAsJToken()
        {
            return JToken.FromObject(currentWeaponConfig.name);
        }

        public void RestoreFromJToken(JToken state)
        {
            EquipWeaponFromName(state.ToObject<string>());
        }

        public IEnumerable<float> GetAdditiveModifiers(Stats.Stats stat)
        {
            if (stat == Stats.Stats.Damage)
            {
                yield return currentWeaponConfig.AdditiveDamage;
            }
        }

        public IEnumerable<float> GetMultiplyingModifiers(Stats.Stats stat)
        {
            if (stat == Stats.Stats.Damage)
            {
                yield return currentWeaponConfig.MultiplierDamage;
            }
        }
    }
}
