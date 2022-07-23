using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using RPG.Core;

namespace RPG.Movement
{
    [RequireComponent(typeof(ActionScheduler))]
    public class Mover : MonoBehaviour, IAction
    {
        ActionScheduler actionScheduler;
        NavMeshAgent nma;
        Health health;

        void Awake()
        {
            nma = GetComponent<NavMeshAgent>();
            actionScheduler = GetComponent<ActionScheduler>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            nma.enabled = !health.IsDead;
            SetAnimationBlend();
        }

        public void StartMoveAction(Vector3 destination)
        {
            actionScheduler.StartAction(this);
            SetDestination(destination);
        }

        public void SetDestination(Vector3 destination)
        {
            nma.destination = destination;
            nma.isStopped = false;
        }

        public void Stop()
        {
            nma.isStopped = true;
        }

        public void Cancel()
        {
            Stop();
        }

        /// <summary>
        /// Sets animation forward-movement based on local forward speed.
        /// </summary>
        void SetAnimationBlend()
        {
            var velocity = nma.velocity;
            var localVelocity = transform.InverseTransformVector(velocity);
            GetComponentInChildren<Animator>().SetFloat("forwardSpeed", localVelocity.z);
        }
    }
}