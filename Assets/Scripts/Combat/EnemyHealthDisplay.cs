using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using RPG.Attributes;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter player;
        TMP_Text healthText;

        void Start()
        {
            player = GameObject.FindWithTag("Player").GetComponent<Fighter>();
            healthText = GetComponent<TMP_Text>();
        }

        void Update()
        {
            Health targetHealth = player.Target;
            if (targetHealth == null) healthText.text = "N/A";
            else healthText.text = $"{targetHealth.GetHealth():0}/{targetHealth.GetMaxHealth():0}"; // Remove decimal places
        }
    }
}
