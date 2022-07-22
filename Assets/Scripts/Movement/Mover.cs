using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using RPG.Combat;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] Transform target;

        NavMeshAgent nma;

        void Awake()
        {
            nma = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            SetAnimationBlend();
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<Fighter>().Cancel();
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