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
            FileStream fs = File.Open(path, FileMode.Create);
            byte[] text = Encoding.UTF8.GetBytes("¡Hola Mundo!");
            fs.Write(text, 0, text.Length);
            fs.Close();
        }
    
        public void Load(string saveName)
        {
            print("Load from " + saveName);
        }

        string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + fileExtension);
        }
    }
}
