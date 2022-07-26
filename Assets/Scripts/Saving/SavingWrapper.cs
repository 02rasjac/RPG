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

        void Awake()
        {
            savingSystem = GetComponent<SavingSystem>();
        }

        // Update is called once per frame
        void Update()
        {
            if (save.WasPressedThisFrame())
            {
                savingSystem.Save(savingSystem.GetPathFromSaveFile(defaultFileName));
            }
            else if (load.WasPressedThisFrame())
            {
                savingSystem.Load(savingSystem.GetPathFromSaveFile(defaultFileName));
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
