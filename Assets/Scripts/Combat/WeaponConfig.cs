using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using RPG.Attributes;
using GameDevTV.Inventories;
using RPG.Stats;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Weapon", order = 0)]
    public class WeaponConfig : EquipableItem, IModifierProvider
    {
        [Header("Weapon stats")]
        [SerializeField] float range = 2f;
        public float Range => range;

        [SerializeField] float additiveDamage = 5f;

        [Tooltip("1 is no change, < 1 is less damage and > 1 is more damage.")]
        [SerializeField] float multiplierDamage = 1f;

        [SerializeField] float timeBetweenAttacks = 1f;
        public float TimeBetweenAttacks => timeBetweenAttacks;

        [SerializeField] bool isRightHand = true;

        [Header("Weapon references")]
        [SerializeField] Weapon equippedPrefab = null;
        [SerializeField] GameObject projectile = null;
        [Tooltip("Override controller for weapn.")]
        [SerializeField] AnimatorOverrideController weaponOverride = null;

        const string weaponName = "Weapon";

        public Weapon Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            Weapon weapon = null;
            if (equippedPrefab != null)
            {
                DestroyOldWeapon(rightHand, leftHand);
                weapon = Instantiate(equippedPrefab, GetHandTrans(rightHand, leftHand));
                weapon.gameObject.name = weaponName;
            }
            if (weaponOverride != null)
                animator.runtimeAnimatorController = weaponOverride;
            else
                Debug.LogWarning("No weapon override controller assigned to " + this.name + ".");

            return weapon;
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instigator, float damage)
        {
            GameObject proj = Instantiate(projectile, GetHandTrans(rightHand, leftHand).position, Quaternion.identity);
            proj.GetComponent<Projectile>().SetTarget(target, instigator, damage);
        }

        void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if (oldWeapon == null) oldWeapon = leftHand.Find(weaponName);
            if (oldWeapon == null) return;

            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }

        Transform GetHandTrans(Transform rightHand, Transform leftHand)
        {
            return isRightHand ? rightHand : leftHand;
        }

        public IEnumerable<float> GetAdditiveModifiers(Stats.Stats stat)
        {
            if (stat == Stats.Stats.Damage)
                yield return additiveDamage;
        }

        public IEnumerable<float> GetMultiplyingModifiers(Stats.Stats stat)
        {
            if (stat == Stats.Stats.Damage)
                yield return multiplierDamage;
        }
    }
}
