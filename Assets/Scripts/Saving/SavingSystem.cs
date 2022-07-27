using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        //TODO: CONVERT FROM BinaryFormatter.Serialize TO USING JSON. CURRENT SAVINGSYSTEM IS **NOT SAFE**!!!
        //TODO: https://gitlab.com/Mnemoth42/RPG/-/wikis/Json%201%20Introduction%20and%20Installation

        const string fileExtension = ".sav";

        public void Save(string saveName)
        {
            string path = GetPathFromSaveFile(saveName);
            print("Save to " + path);
            using (FileStream fs = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, CaptureState());
            }
        }
    
        public void Load(string saveName)
        {
            string path = GetPathFromSaveFile(saveName);
            print("Load from " + path);
            using (FileStream fs = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                LoadState(formatter.Deserialize(fs));
            }
        }

        object CaptureState()
        {
            Dictionary<string, object> states = new Dictionary<string, object>();
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                states[saveable.GetUniqueID()] = saveable.CaptureState();
            }
            return states;
        }

        void LoadState(object state)
        {
            Dictionary<string, object> states = (Dictionary<string, object>)state;
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                saveable.LoadState(states[saveable.GetUniqueID()]);
            }
        }

        string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + fileExtension);
        }
    }
}
