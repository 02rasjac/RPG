using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Saving;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
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

        public object CaptureState()
        {
            return health;
        }

        public void LoadState(object state)
        {
            health = (float)state;
            if (health <= 0)
                Die(true);
            else
                isDead = false;
        }

        void Die(bool instantDeath = false)
        {
            if (isDead) return;
            if (instantDeath) // Don't run deathanimation if dead on load.
            {
                GetComponent<Animator>().Play("Death", 0, 1);
            }
            else
            {
                GetComponent<Animator>().SetTrigger("die");
                GetComponent<ActionScheduler>().CancelCurrentAction();
            }

            isDead = true;
        }
    }
}