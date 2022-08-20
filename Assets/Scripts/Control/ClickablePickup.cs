using GameDevTV.Inventories;
using RPG.Core;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    [RequireComponent(typeof(Pickup))]
    public class ClickablePickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] CursorType pickupCursor;
        [SerializeField] CursorType inventoryFullCursor;
        Pickup pickup;

        private void Awake()
        {
            pickup = GetComponent<Pickup>();
        }

        private void OnTriggerEnter(Collider other)
        {
            pickup.PickupItem();
        }

        public CursorType GetCursorType()
        {
            if (pickup.CanBePickedUp())
            {
                return pickupCursor;
            }
            else
            {
                return inventoryFullCursor;
            }
        }

        public bool HandleRaycast(GameObject caller, bool isPressed)
        {
            if (isPressed)
            {
                caller.GetComponent<Mover>().SetDestination(transform.position);
            }
            return true;
        }
    }
}