using System.Collections;
using UnityEngine;

namespace RPG.Saving
{
    public class SaveableEntity : MonoBehaviour
    {
        public string GetUniqueID()
        {
            return "";
        }

        public object CaptureState()
        {
            print("Capturing state for " + GetUniqueID());
            return null;
        }

        public void LoadState(object state)
        {
            print("Loading state for " + GetUniqueID());
        }
    }
}