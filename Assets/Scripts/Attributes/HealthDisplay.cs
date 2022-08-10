using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        // For now, only works for players health
        Health health;
        TMP_Text healthText;

        void Start()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            healthText = GetComponent<TMP_Text>();
        }

        void Update()
        {
            healthText.text = $"{health.GetHealthPercentage():0}%"; // Remove decimal places
        }
    }
}
