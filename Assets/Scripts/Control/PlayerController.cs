using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

using RPG.Attributes;
using RPG.Movement;
using RPG.Core;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
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
            RaycastHit hit;
            if (Physics.Raycast(GetMouseRay(), out hit))
            {
                if (click.IsPressed())
                {
                    mover.StartMoveAction(hit.point);
                }
                moveCursors.SetAsCursor();
                return true;
            }
            return false;
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