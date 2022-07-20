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
        nma.destination = target.position;
    }
}
