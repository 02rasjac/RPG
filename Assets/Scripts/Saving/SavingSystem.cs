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
            Dictionary<string, object> state = LoadFile(saveName);
            CaptureState(state);
            SaveFile(saveName, CaptureState(state));
        }
    
        public void Load(string saveName)
        {
            LoadState(LoadFile(saveName));
        }

        void SaveFile(string fileName, object state)
        {
            string path = GetPathFromSaveFile(fileName);
            print("Save to " + path);
            using (FileStream fs = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, state);
            }
        }

        Dictionary<string, object> LoadFile(string saveName)
        {
            string path = GetPathFromSaveFile(saveName);
            print("Load from " + path);
            if (!File.Exists(path)) return new Dictionary<string, object>();
            using (FileStream fs = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(fs);
            }
        }

        Dictionary<string, object> CaptureState(Dictionary<string, object> state)
        {
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                state[saveable.GetUUID()] = saveable.CaptureState();
            }
            return state;
        }

        void LoadState(Dictionary<string, object> states)
        {
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                var id = saveable.GetUUID();
                if (states.ContainsKey(id))
                {
                    saveable.LoadState(states[id]);
                }
            }
        }

        string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + fileExtension);
        }
    }
}
