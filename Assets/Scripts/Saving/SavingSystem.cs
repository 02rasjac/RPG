using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        public void Save(string fileName)
        {
            print("Save to " + fileName);
        }
    
        public void Load(string fileName)
        {
            print("Load from " + fileName);
        }
    }
}
