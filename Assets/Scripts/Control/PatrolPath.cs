using System.Collections;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        const float visualiseRadius = 0.5f;

        void OnDrawGizmos()
        {
            RenderEachWaypoint(0.3f);
        }

        void OnDrawGizmosSelected()
        {
            RenderEachWaypoint(1.0f);
        }

        public Vector3 GetChildPosition(int index)
        {
            return transform.GetChild(index).position;
        }

        public int GetNextIndex(int i)
        {
            return (i == transform.childCount - 1) ? 0 : (i + 1);
        }

        void RenderEachWaypoint(float alpha)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Color c = i == 0 ? Color.green : Color.white;
                c.a = alpha;
                Gizmos.color = c;
                Gizmos.DrawSphere(GetChildPosition(i), visualiseRadius);
                Gizmos.DrawLine(GetChildPosition(i), GetChildPosition(GetNextIndex(i)));
            }
        }
    }
}