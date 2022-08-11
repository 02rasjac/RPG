using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG.Attributes
{
    public class ExperienceDisplay : MonoBehaviour
    {
        // For now, only works for players health
        Experience xp;
        TMP_Text xpText;

        void Start()
        {
            xp = GameObject.FindWithTag("Player").GetComponent<Experience>();
            xpText = GetComponent<TMP_Text>();
        }

        void Update()
        {
            xpText.text = $"{xp.GetExperience():0}"; // Remove decimal places
        }
    }
}
