using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 100f;

        bool isDead = false;
        public bool IsDead { get { return isDead; } }

        public void TakeDamage(float ammount)
        {
            health -= ammount;
            if (health <= 0)
            {
                health = 0;
                Die();
            }
        }

        void Die()
        {
            if (!isDead)
            {
                GetComponent<Animator>().SetTrigger("die");
                GetComponent<ActionScheduler>().CancelCurrentAction();
                isDead = true;
            }
        }
    }
}