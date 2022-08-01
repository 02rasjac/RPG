using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 5f;

        Transform target;

        void Start()
        {
            target = GameObject.FindWithTag("Player").transform;
        }

        // Update is called once per frame
        void Update()
        {
            if (target == null) return;
            Collider collider = target.GetComponent<Collider>();
            if (collider != null) transform.LookAt(collider.bounds.center);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
