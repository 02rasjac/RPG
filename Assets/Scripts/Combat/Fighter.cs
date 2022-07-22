using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Core;
using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float weaponRange = 2f;

        Mover mover;
        ActionScheduler actionScheduler;

        Transform target;

        void Awake()
        {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
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
                }
            }
        }
        
        public void Attack(CombatTarget target)
        {
            actionScheduler.StartAction(this);
            this.target = target.transform;
        }

        public void Cancel()
        {
            target = null;
        }
    }
}
