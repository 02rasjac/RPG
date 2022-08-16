using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health characterHealth;
        [SerializeField] RectTransform foreground;

        void OnEnable()
        {
            characterHealth.takeDamage += UpdateBar;
        }

        void OnDisable()
        {
            characterHealth.takeDamage -= UpdateBar;
        }

        void UpdateBar()
        {
            foreground.localScale = new Vector3(characterHealth.GetHealthFraction(), 1f, 1f);
        }
    }
}
