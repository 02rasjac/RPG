using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameDevTV.UI
{
    public class ShowHideUI : MonoBehaviour
    {
        [SerializeField] InputAction toggleKey;
        [SerializeField] GameObject uiContainer = null;

        // Start is called before the first frame update
        void Start()
        {
            uiContainer.SetActive(false);
        }

        void OnEnable()
        {
            toggleKey.Enable();
        }
        
        void OnDisable()
        {
            toggleKey.Disable();
        }

        // Update is called once per frame
        void Update()
        {
            if (toggleKey.WasPerformedThisFrame())
            {
                uiContainer.SetActive(!uiContainer.activeSelf);
            }
        }
    }
}