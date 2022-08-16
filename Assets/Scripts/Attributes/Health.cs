using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using RPG.Core;
using RPG.Saving;
using Newtonsoft.Json.Linq;
using RPG.Stats;
using System;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] Color healColor = Color.green;
        [SerializeField] Color damageColor = Color.red;
        [SerializeField] UnityEvent<float> onTakeDamage;
        [SerializeField] UnityEvent<float, Color> onTakeDamageColor;
        [SerializeField] UnityEvent<float, Color> onHealColor;

        float health = 100f;
        bool isDead = false;
        public bool IsDead { get { return isDead; } }

        BaseStats baseStats;

        void Awake()
        {
            baseStats = GetComponent<BaseStats>();
        }

        void Start()
        {
            health = baseStats.GetBaseStat(Stats.Stats.Health);
        }

        void OnEnable()
        {
            baseStats.OnLevelUp += HealFromLevelling;
        }

        void OnDisable()
        {
            baseStats.OnLevelUp -= HealFromLevelling;
        }

        public void TakeDamage(GameObject instigator, float ammount)
        {
            health -= ammount;
            onTakeDamage?.Invoke(ammount);
            onTakeDamageColor?.Invoke(ammount, damageColor);
            if (health <= 0)
            {
                health = 0;
                Experience instigatorXP = instigator.GetComponent<Experience>();
                if (instigatorXP != null) instigatorXP.GainExperience(baseStats.GetBaseStat(Stats.Stats.ExperienceReward));
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

        public float GetHealthPercentage() => 100 * GetHealthFraction();

        public float GetHealthFraction() => health / GetComponent<BaseStats>().GetStat(Stats.Stats.Health);

        public float GetHealth() => health;

        public float GetMaxHealth() => baseStats.GetStat(Stats.Stats.Health);

        void HealFromLevelling(int oldLevel)
        {
            float regeneratedHealth = GetMaxHealth() * (baseStats.levelUpHealPercentage * 0.01f);
            if (regeneratedHealth > health)
            {
                onHealColor?.Invoke(regeneratedHealth - health, healColor);
                health = regeneratedHealth;
            }
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