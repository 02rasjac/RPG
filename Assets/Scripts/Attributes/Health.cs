using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Core;
using RPG.Saving;
using Newtonsoft.Json.Linq;
using RPG.Stats;
using System;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float health = 100f;

        bool isDead = false;
        public bool IsDead { get { return isDead; } }

        BaseStats baseStats;

        void Start()
        {
            baseStats = GetComponent<BaseStats>();
            baseStats.OnLevelUp += HealFromLevelling;
            health = baseStats.GetStat(Stats.Stats.Health);
        }

        public void TakeDamage(GameObject instigator, float ammount)
        {
            health -= ammount;
            if (health <= 0)
            {
                health = 0;
                Experience instigatorXP = instigator.GetComponent<Experience>();
                if (instigatorXP != null) instigatorXP.GainExperience(baseStats.GetStat(Stats.Stats.ExperienceReward));
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

        public float GetHealthPercentage() => 100 * (health / GetComponent<BaseStats>().GetStat(Stats.Stats.Health));

        void HealFromLevelling(int oldLevel)
        {
            float regeneratedHealth = baseStats.GetStat(Stats.Stats.Health) * (baseStats.levelUpHealPercentage * 0.01f);
            health = Mathf.Max(regeneratedHealth, health);
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