using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Core;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 5f;
        [Tooltip("Ammount of speed the projectile loses per second.")]
        [SerializeField] float speedLossFactor = 1f;

        float damage;

        Health target;

        void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            speed -= speedLossFactor * Time.deltaTime;
            if (speed < 0f) Destroy(this.gameObject);
        }

        void OnTriggerEnter(Collider other)
        {
            // Other should take damage nomatter if it's the target or not, as long as it has health.
            Health health = other.GetComponent<Health>();
            if (health == null || target.IsDead) return;
            health.TakeDamage(damage);
            Destroy(this.gameObject);
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
            Collider collider = target.GetComponent<Collider>();
            if (collider != null) transform.LookAt(collider.bounds.center);
        }
    }
}
