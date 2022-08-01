using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickups : MonoBehaviour
    {
        [SerializeField] Weapon weapon;
        
        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            other.GetComponent<Fighter>().EquipWeapon(weapon);
            Destroy(this.gameObject);
        }
    }
}
