using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

using RPG.Core;
using System.Collections.Generic;

namespace RPG.Saving
{
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField] string UUID;

        static Dictionary<string, SaveableEntity> globalLookup = new Dictionary<string, SaveableEntity>();
       
        public string GetUUID()
        {
            return UUID;
        }

        public object CaptureState()
        {
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach (var saveable in GetComponents<ISaveable>())
            {
                state[saveable.GetType().ToString()] = saveable.CaptureState();
            }
            return state;
        }

        public void LoadState(object state)
        {
            Dictionary<string, object> stateDict = (Dictionary<string, object>)state;
            foreach (var saveable in GetComponents<ISaveable>())
            {
                string key = saveable.GetType().ToString();
                if (stateDict.ContainsKey(key))
                {
                    saveable.LoadState(stateDict[key]);
                }
            }
        }

        bool IsUnique(string s)
        {
            if (!globalLookup.ContainsKey(s)) return true;
            if (globalLookup[s] == this) return true;
            if (globalLookup[s] == null || globalLookup[s].GetUUID() != s)
            { // Existing object is deleted => this becomes unique
                globalLookup.Remove(s);
                return true;
            }

            return false;
        }

#if UNITY_EDITOR
        void Update()
        {
            if (Application.isPlaying) return;
            if (string.IsNullOrEmpty(gameObject.scene.path)) return;

            SerializedObject obj = new SerializedObject(this);
            SerializedProperty prop = obj.FindProperty("UUID");
            if (string.IsNullOrEmpty(prop.stringValue) || !IsUnique(prop.stringValue))
            {
                prop.stringValue = System.Guid.NewGuid().ToString();
                obj.ApplyModifiedProperties();
            }

            globalLookup[UUID] = this;
        }
    }
#endif
}