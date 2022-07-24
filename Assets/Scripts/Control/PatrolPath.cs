using System.Collections;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        const float visualiseRadius = 0.5f;

        void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                Gizmos.color = Color.white;
                Gizmos.DrawSphere(child.position, visualiseRadius);
            }
        }
    }
}