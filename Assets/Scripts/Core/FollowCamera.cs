using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform target;

    void LateUpdate()
    {
        // Do this in LateUpdate to prevent potential jittering
        transform.position = target.position;
    }
}
