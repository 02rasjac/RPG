using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Saving;
using Newtonsoft.Json.Linq;

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

        public JToken CaptureAsJToken()
        {
            return JToken.FromObject(health);
        }

        public void RestoreFromJToken(JToken state)
        {
            health = state.ToObject<float>();
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