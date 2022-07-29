using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/weapon", order = 0)]
    internal class Weapon : ScriptableObject
    {
        [SerializeField] GameObject weaponPrefab = null;
        [Tooltip("Override controller for weapn.")]
        [SerializeField] AnimatorOverrideController weaponOverride = null;

        public void Spawn(Transform handTrans, Animator animator)
        {
            Instantiate(weaponPrefab, handTrans);
            animator.runtimeAnimatorController = weaponOverride;

        }
    }
}
