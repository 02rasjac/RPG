using System.Collections;
using UnityEngine;

using RPG.Attributes;
using RPG.Core;

namespace RPG.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        public bool HandleRaycast(GameObject caller, bool isPressed)
        {
            Fighter fighter = caller.GetComponent<Fighter>();
            if (!fighter.CanAttack(gameObject)) return false;

            if (isPressed)
            {
                fighter.Attack(gameObject);
            }
            return true;
        }
    }
}