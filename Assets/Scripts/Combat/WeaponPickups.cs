using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickups : MonoBehaviour
    {
        [SerializeField] Weapon weapon;
        [SerializeField] float respawnTime = 5f;
        
        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
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
