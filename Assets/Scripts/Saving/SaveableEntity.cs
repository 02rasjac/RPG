using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

using RPG.Core;

namespace RPG.Saving
{
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField] string UUID;
       
        public string GetUUID()
        {
            return UUID;
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void LoadState(object state)
        {
            NavMeshAgent nma = GetComponent<NavMeshAgent>();
            ActionScheduler actionScheduler = GetComponent<ActionScheduler>();

            if (nma != null)
            {
                nma.Warp(((SerializableVector3)state).ToVector3());
            }
            if (actionScheduler != null)
            {
                actionScheduler.CancelCurrentAction();
            }
        }

#if UNITY_EDITOR
        void Update()
        {
            if (Application.isPlaying) return;
            if (string.IsNullOrEmpty(gameObject.scene.path)) return;

            SerializedObject obj = new SerializedObject(this);
            SerializedProperty prop = obj.FindProperty("UUID");
            if (!string.IsNullOrEmpty(prop.stringValue)) return;

            prop.stringValue = System.Guid.NewGuid().ToString();
            obj.ApplyModifiedProperties();
        }
    }
#endif
}