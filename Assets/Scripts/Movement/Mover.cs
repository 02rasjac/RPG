using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using RPG.Core;
using RPG.Saving;
using Newtonsoft.Json.Linq;

namespace RPG.Movement
{
    [RequireComponent(typeof(ActionScheduler))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class Mover : MonoBehaviour, IAction, ISaveable
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

        public void SetSpeed(float speed)
        {
            nma.speed = Mathf.Abs(speed);
        }

        public void Stop()
        {
            nma.isStopped = true;
        }

        public void Cancel()
        {
            Stop();
        }

        public JToken CaptureAsJToken()
        {
            return transform.position.ToToken();
        }

        public void RestoreFromJToken(JToken state)
        {
            nma.Warp(state.ToVector3());
            actionScheduler.CancelCurrentAction();
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