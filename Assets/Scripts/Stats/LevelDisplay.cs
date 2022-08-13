using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats bs;
        TMP_Text levelText;

        void Start()
        {
            bs = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
            levelText = GetComponent<TMP_Text>();
        }

        void Update()
        {
            levelText.text = $"{bs.CurrentLevel:0}"; // Remove decimal places
        }
    }
}
