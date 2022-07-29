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

        void Awake()
        {
            savingSystem = GetComponent<SavingSystem>();
        }

        IEnumerator Start()
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
            {
                Save();
            }
            else if (load.WasPressedThisFrame())
            {
                Load();
            }
        }

        void OnEnable()
        {
            save.Enable();
            load.Enable();
        }

        void OnDisable()
        {
            save.Disable();
            load.Disable();
        }
    }
}