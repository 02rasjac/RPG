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
                byte[] buffer = SerializeVector(GetPlayerTransform().position);
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
                GetPlayerTransform().position = DeserializeVector(buffer);
            }
        }

        Transform GetPlayerTransform()
        {
            return GameObject.FindWithTag("Player").transform;
        }

        byte[] SerializeVector(Vector3 v)
        {
            ushort floatSize = sizeof(float);
            byte[] buffer = new byte[3 * floatSize];
            System.BitConverter.GetBytes(v.x).CopyTo(buffer, 0);
            System.BitConverter.GetBytes(v.y).CopyTo(buffer, floatSize);
            System.BitConverter.GetBytes(v.z).CopyTo(buffer, 2*floatSize);

            return buffer;
        }

        Vector3 DeserializeVector(byte[] buffer)
        {
            Vector3 v = new Vector3();
            ushort floatSize = sizeof(float);
            v.x = System.BitConverter.ToSingle(buffer, 0);
            v.y = System.BitConverter.ToSingle(buffer, floatSize);
            v.z = System.BitConverter.ToSingle(buffer, 2 * floatSize);

            return v;
        }

        string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + fileExtension);
        }
    }
}
