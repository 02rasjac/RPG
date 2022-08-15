using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using RPG.Attributes;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Cursors")]
        [SerializeField] CursorType noneCursors;
        [SerializeField] CursorType moveCursors;
        [SerializeField] CursorType attackCursors;
        [SerializeField] InputAction click;

        Mover mover;
        Fighter fighter;
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
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            if (!health.IsDead)
            {
                if (InteractWithCombat()) return;
                if (InteractWithMovement()) return;
                noneCursors.SetCursor();
            }
        }

        bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (var hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;
                if (!fighter.CanAttack(target.gameObject)) continue;

                if (click.IsPressed())
                {
                    fighter.Attack(target.gameObject);
                }
                attackCursors.SetCursor();
                return true;
            }
            return false;
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
                moveCursors.SetCursor();
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