using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

using RPG.Attributes;
using RPG.Movement;
using RPG.Core;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Nav Mesh Settings")]
        [SerializeField] float maxSampleDistance = 1f;
        [SerializeField] float maxPathDistance = 40f;
        [Header("Cursors")]
        [SerializeField] CursorType noneCursors;
        [SerializeField] CursorType uiCursors;
        [SerializeField] CursorType moveCursors;
        [SerializeField] InputAction click;

        Mover mover;
        Health health;

        void OnEnable()
        {
            click.Enable();
        }
    
        void OnDisable()
        {
            click.Disable();
            noneCursors.SetAsCursor();
        }

        void Awake()
        {
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            if (InteractWithUI()) return;
            if (health.IsDead)
            {
                noneCursors.SetAsCursor();
                return;
            }
            if (InteractWithComponent()) return;
            if (InteractWithMovement()) return;

            noneCursors.SetAsCursor();
        }

        bool InteractWithComponent()
        {
            RaycastHit[] hits = GetSortedRaycasts();
            foreach (var hit in hits)
            {
                var raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach (var raycastable in raycastables)
                {
                    if (raycastable.HandleRaycast(gameObject, click.IsPressed()))
                    {
                        raycastable.GetCursorType().SetAsCursor();
                        return true;
                    }
                }
            }
            return false;

            RaycastHit[] GetSortedRaycasts()
            {
                RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
                float[] distances = new float[hits.Length];
                for (int i = 0; i < hits.Length; i++)
                {
                    distances[i] = hits[i].distance;
                }
                Array.Sort(distances, hits);
                return hits;
            }
        }

        bool InteractWithMovement()
        {
            Vector3 target;
            if (RaycastNavmesh(out target))
            {
                if (click.IsPressed())
                {
                    mover.StartMoveAction(target);
                }
                moveCursors.SetAsCursor();
                return true;
            }
            return false;

            bool RaycastNavmesh(out Vector3 target)
            {
                target = new Vector3();
                RaycastHit rayHit;
                if (!Physics.Raycast(GetMouseRay(), out rayHit)) return false;

                NavMeshHit nmHit;
                if (!NavMesh.SamplePosition(rayHit.point, out nmHit, maxSampleDistance, NavMesh.AllAreas)) return false;
                target = nmHit.position;

                NavMeshPath path = new NavMeshPath();
                NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, path);
                if (path.status != NavMeshPathStatus.PathComplete) return false;

                if (CalculatePathDistance(path) > maxPathDistance) return false;

                return true;
            }

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

        bool InteractWithUI()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                uiCursors.SetAsCursor();
                return true;
            }
            return false;
        }

        Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        }
    }
}