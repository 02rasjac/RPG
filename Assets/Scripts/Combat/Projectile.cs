using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Core;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 5f;

        Health target;

        void Update()
        {
            if (target == null) return;
            Collider collider = target.GetComponent<Collider>();
            if (collider != null) transform.LookAt(collider.bounds.center);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget(Health target)
        {
            this.target = target;
        }
    }
}
