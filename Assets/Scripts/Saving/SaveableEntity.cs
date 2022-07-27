using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            print("Capturing state for " + GetUUID());
            return null;
        }

        public void LoadState(object state)
        {
            print("Loading state for " + GetUUID());
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