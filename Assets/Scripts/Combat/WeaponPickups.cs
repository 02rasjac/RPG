using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Core;
using RPG.Movement;

namespace RPG.Combat
{
    public class WeaponPickups : MonoBehaviour, IRaycastable
    {
        [SerializeField] Weapon weapon;
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
            other.GetComponent<Fighter>().EquipWeapon(weapon);
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
