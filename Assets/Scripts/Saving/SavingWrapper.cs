using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Saving
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

        void Start()
        {
            StartCoroutine(savingSystem.LoadLastScene(defaultFileName));
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
