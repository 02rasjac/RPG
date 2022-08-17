using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Core;
using RPG.Movement;
using RPG.Attributes;

namespace RPG.Combat
{
    public class WeaponPickups : MonoBehaviour, IRaycastable
    {
        [SerializeField] WeaponConfig weapon;
        [SerializeField] float healAmount = 0;
        [SerializeField] float respawnTime = 5f;
        [SerializeField] CursorType cursor;
        
        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            PickUp(other);
        }

        public CursorType GetCursorType()
        {
            return cursor;
        }

        public bool HandleRaycast(GameObject caller, bool isPressed)
        {
            if (isPressed)
            {
                caller.GetComponent<Mover>().SetDestination(transform.position);
            }
            return true;
        }

        void PickUp(Collider other)
        {
            if (weapon != null) 
                other.GetComponent<Fighter>().EquipWeapon(weapon);
            if (healAmount > 0)
                other.GetComponent<Health>().Heal(healAmount);
            StartCoroutine(HideAndShow());
        }

        IEnumerator HideAndShow()
        {
            SetActiveAndEnable(false);
            yield return new WaitForSeconds(respawnTime);
            SetActiveAndEnable(true);

            void SetActiveAndEnable(bool value)
            {
                GetComponent<Collider>().enabled = value;
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(value);
                }
            }
        }
    }
}
