using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] InputAction click;

        Mover mover;
        Fighter fighter;

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
            fighter = GetComponent<Fighter>();
        }

        void Update()
        {
            InteractWithCombat();
            InteractWithMovement();
        }

        void InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (var hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (click.WasPressedThisFrame())
                {
                    fighter.Attack(target);
                }
            }
        }

        void InteractWithMovement()
        {
            if (click.IsPressed())
            {
                MoveToCursor();
            }
        }

        void MoveToCursor()
        { 
            RaycastHit hit;
            if (Physics.Raycast(GetMouseRay(), out hit))
            {
                mover.SetDestination(hit.point);
            }
        }

        Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        }
    }
}