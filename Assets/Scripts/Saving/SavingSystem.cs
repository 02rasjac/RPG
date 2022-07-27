using System.Collections;
using System.Collections.Generic;
using System.IO;
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
                byte[] buffer = Encoding.UTF8.GetBytes("¡Hola Mundo!");
                fs.Write(buffer, 0, buffer.Length);
            }
        }
    
        public void Load(string saveName)
        {
            string path = GetPathFromSaveFile(saveName);
            print("Load from " + path);
            using (FileStream fs = File.Open(path, FileMode.Open))
            {
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                print(Encoding.UTF8.GetString(buffer));
            }
        }

        string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + fileExtension);
        }
    }
}
