using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        const string fileExtension = ".sav";

        public void Save(string fileName)
        {
            print("Save to " + fileName);
        }
    
        public void Load(string fileName)
        {
            print("Load from " + fileName);
        }

        public string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + fileExtension);
        }
    }
}
