using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health characterHealth;
        [SerializeField] RectTransform foreground;

        Canvas canvas;

        void Start()
        {
            canvas = GetComponentInChildren<Canvas>();
            canvas.enabled = false;
        }

        public void UpdateBar(float amount)
        {
            float frac = characterHealth.GetHealthFraction();
            if (frac <= Mathf.Epsilon || Mathf.Approximately(frac, 1f)) 
                canvas.enabled = false;
            else
                canvas.enabled = true;
            foreground.localScale = new Vector3(frac, 1f, 1f);
        }
    }
}
