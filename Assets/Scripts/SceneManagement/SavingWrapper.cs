using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        [SerializeField] InputAction save;
        [SerializeField] InputAction load;
        [SerializeField] InputAction delete;

        const string defaultFileName = "save";

        SavingSystem savingSystem;

        public void Save()
        {
            savingSystem.Save(defaultFileName);
        }

        public void Load()
        {
            savingSystem.Load(defaultFileName);
        }

        public void Delete()
        {
            savingSystem.Delete(defaultFileName);
        }

        void Awake()
        {
            savingSystem = GetComponent<SavingSystem>();
            StartCoroutine(LoadLastScene());
        }

        IEnumerator LoadLastScene() 
        {
            var fader = FindObjectOfType<Fader>();
            fader.FadeOutInstant();
            yield return StartCoroutine(savingSystem.LoadLastScene(defaultFileName));
            yield return fader.FadeIn();
        }

        // Update is called once per frame
        void Update()
        {
            if (save.WasPressedThisFrame())
                Save();
            else if (load.WasPressedThisFrame())
                Load();
            else if (delete.WasPressedThisFrame())
                Delete();
                
        }

        void OnEnable()
        {
            save.Enable();
            load.Enable();
            delete.Enable();
        }

        void OnDisable()
        {
            save.Disable();
            load.Disable();
            delete.Disable();
        }
    }
}
