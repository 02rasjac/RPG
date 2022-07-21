using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputAction moveTo;
    Mover mover;

    void OnEnable()
    {
        moveTo.Enable();
    }
    
    void OnDisable()
    {
        moveTo.Disable();
    }

    void Awake()
    {
        mover = GetComponent<Mover>();
    }

    void Update()
    {
        if (moveTo.IsPressed())
        {
            MoveToCursor();
        }
    }

    void MoveToCursor()
    { 
        RaycastHit hit;
        if (CastRay(out hit))
        {
            mover.SetDestination(hit.point);
        }
    }

    /// <summary>
    /// Cast a ray from the camera through the mouseclick and calculate the hitpoint <c>hit</c>.
    /// </summary>
    /// <param name="hit">Store hit-information.</param>
    /// <returns>True if raycast hit something.</returns>
    bool CastRay(out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        return Physics.Raycast(ray, out hit);
    }
}
