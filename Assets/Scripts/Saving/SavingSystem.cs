using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        [SerializeField] SavingStrategy strategy;


        /// <summary>
        /// Will load the last scene that was saved and restore the state. This
        /// must be run as a coroutine.
        /// </summary>
        /// <param name="saveFile">The save file to consult for loading.</param>
        public IEnumerator LoadLastScene(string saveFile)
        {
            JObject state = LoadJsonFromFile(saveFile);
            IDictionary<string, JToken> stateDict = state;
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            if (stateDict.ContainsKey("lastSceneBuildIndex"))
            {
                buildIndex = (int)stateDict["lastSceneBuildIndex"];
            }

            var asyncLoad = SceneManager.LoadSceneAsync(buildIndex);
            while (!asyncLoad.isDone) yield return null;

            RestoreFromToken(state);
        }

        /// <summary>
        /// Save the current scene to the provided save file.
        /// </summary>
        public void Save(string saveFile)
        {
            JObject state = LoadJsonFromFile(saveFile);
            CaptureAsToken(state);
            SaveFileAsJSon(saveFile, state);
        }

        /// <summary>
        /// Delete the state in the given save file.
        /// </summary>
        public void Delete(string saveFile)
        {
            print("Deleting " + saveFile);
            File.Delete(GetPathFromSaveFile(saveFile));
        }

        public void Load(string saveFile)
        {
            RestoreFromToken(LoadJsonFromFile(saveFile));
        }

        public IEnumerable<string> ListSaves()
        {
            foreach (string path in Directory.EnumerateFiles(Application.persistentDataPath))
            {
                if (Path.GetExtension(path) == strategy.GetExtension())
                {
                    yield return Path.GetFileNameWithoutExtension(path);
                }
            }
        }

        JObject LoadJsonFromFile(string saveFile)
        {
            return strategy.LoadFromFile(saveFile);
        }

        void SaveFileAsJSon(string saveFile, JObject state)
        {
            strategy.SaveToFile(saveFile, state);
        }


        void CaptureAsToken(JObject state)
        {
            IDictionary<string, JToken> stateDict = state;
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                stateDict[saveable.GetUniqueIdentifier()] = saveable.CaptureAsJtoken();
            }

            stateDict["lastSceneBuildIndex"] = SceneManager.GetActiveScene().buildIndex;
        }


        void RestoreFromToken(JObject state)
        {
            IDictionary<string, JToken> stateDict = state;
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                string id = saveable.GetUniqueIdentifier();
                if (stateDict.ContainsKey(id))
                {
                    saveable.RestoreFromJToken(stateDict[id]);
                }
            }
        }


        string GetPathFromSaveFile(string saveFile)
        {
            return strategy.GetPathFromSaveFile(saveFile);
        }


    }
}
