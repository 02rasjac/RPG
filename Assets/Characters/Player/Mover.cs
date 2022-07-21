using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        if (Input.GetMouseButton(0))
        {
            MoveToCursor();
        }

        SetAnimationBlend();
    }

    void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);

        if (hasHit)
        {
            nma.destination = hit.point;
        }
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
