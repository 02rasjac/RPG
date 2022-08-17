using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using RPG.Attributes;
using RPG.Core;
using RPG.Saving;
using Newtonsoft.Json.Linq;

namespace RPG.Movement
{
    [RequireComponent(typeof(ActionScheduler))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float maxPathDistance = 40f;

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

        public bool CanMoveTo(Vector3 destination)
        {
            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, path);
            //if (path.status != NavMeshPathStatus.PathComplete) return false;
            //if (CalculatePathDistance(path) > maxPathDistance) return false;
            //return true;
            return path.status == NavMeshPathStatus.PathComplete && CalculatePathDistance(path) <= maxPathDistance;

            float CalculatePathDistance(NavMeshPath path)
            {
                float distance = 0f;
                for (int i = 1; i < path.corners.Length; i++)
                {
                    distance += Vector3.Distance(path.corners[i - 1], path.corners[i]);
                }
                return distance;
            }
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