using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Attributes;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 5f;
        [Tooltip("Ammount of speed the projectile loses per second.")]
        [SerializeField] float speedLossFactor = 1f;
        [SerializeField] bool isHoming = false;
        [SerializeField] GameObject hitVFX;

        float damage;
        GameObject hitInstance;
        bool hitPlayed = false;

        Health target;
        Collider targetCollider;
        GameObject instigator;

        void Update()
        {
            if (hitInstance == null || !hitInstance.GetComponent<ParticleSystem>().isPlaying)
            {
                MoveProjectile();
                speed -= speedLossFactor * Time.deltaTime;
                if (hitPlayed) Destroy(hitInstance);
                if (speed < 0f) Destroy(this.gameObject);
            }
            else
            {
                hitPlayed = true;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            // Other should take damage nomatter if it's the target or not, as long as it has health.
            Health health = other.GetComponent<Health>();
            if (health == null || health.IsDead) return;
            if (hitVFX != null)
            {
                hitInstance = Instantiate(hitVFX, other.bounds.center, transform.rotation);
            }
            health.TakeDamage(instigator, damage);
            this.transform.parent = other.transform;
            speed = 0f;
        }

        public void SetTarget(Health target, GameObject instigator, float damage)
        {
            this.target = target;
            this.damage = damage;
            this.instigator = instigator;
            targetCollider = target.GetComponent<Collider>();
            if (targetCollider != null) transform.LookAt(targetCollider.bounds.center);
        }

        void MoveProjectile()
        {
            if (isHoming && !target.IsDead && targetCollider != null)
                transform.LookAt(targetCollider.bounds.center);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
