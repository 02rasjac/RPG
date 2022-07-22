using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float weaponRange = 2f;

        Mover mover;

        Transform target;

        void Awake()
        {
            mover = GetComponent<Mover>();
        }

        void Update()
        {
            if (target != null)
            {
                if (weaponRange <= Vector3.Distance(transform.position, target.position))
                {
                    mover.SetDestination(target.position);
                }
                else
                {
                    mover.Stop();
                    target = null;
                }
            }
        }
        public void Attack(CombatTarget target)
        {
            this.target = target.transform;
        }
    }
}
