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
        const string fileExtension = ".sav";

        public void Save(string saveName)
        {
            string path = GetPathFromSaveFile(saveName);
            print("Save to " + path);
            using (FileStream fs = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                SerializableVector3 sv3 = new SerializableVector3(GetPlayerTransform().position);
                formatter.Serialize(fs, sv3);
            }
        }
    
        public void Load(string saveName)
        {
            string path = GetPathFromSaveFile(saveName);
            print("Load from " + path);
            using (FileStream fs = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                SerializableVector3 sv3 = (SerializableVector3)formatter.Deserialize(fs);
                GetPlayerTransform().position = sv3.ToVector3();
            }
        }

        Transform GetPlayerTransform()
        {
            return GameObject.FindWithTag("Player").transform;
        }

        string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + fileExtension);
        }
    }
}
