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
                Gizmos.color = i == 0 ? Color.green : Color.white;
                Gizmos.DrawSphere(GetChildPosition(i), visualiseRadius);
                Gizmos.DrawLine(GetChildPosition(i), GetChildPosition(GetNextIndex(i)));
            }
        }

        Vector3 GetChildPosition(int index)
        {
            return transform.GetChild(index).position;
        }

        int GetNextIndex(int i)
        {
            return (i == transform.childCount - 1) ? 0 : (i + 1);
        }
    }
}