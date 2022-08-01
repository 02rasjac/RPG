using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [Header("Weapon stats")]
        [SerializeField] float range = 2f;
        public float Range => range;

        [SerializeField] float damage = 5f;
        public float Damage => damage;

        [SerializeField] float timeBetweenAttacks = 1f;
        public float TimeBetweenAttacks => timeBetweenAttacks;

        [SerializeField] bool isRightHand = true;

        [Header("Weapon references")]
        [SerializeField] GameObject equippedPrefab = null;
        [Tooltip("Override controller for weapn.")]
        [SerializeField] AnimatorOverrideController weaponOverride = null;

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if (equippedPrefab != null)
                Instantiate(equippedPrefab, (isRightHand ? rightHand : leftHand));
            if (weaponOverride != null)
                animator.runtimeAnimatorController = weaponOverride;
        }
    }
}
