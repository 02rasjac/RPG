using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG.UI
{
    public class CharacterFadingTextSpawner : MonoBehaviour
    {
        [SerializeField] GameObject fadingTextPrefab;
        [SerializeField] float destroyTextAfter = 5f;

        public void Spawn(float value, Color textColor)
        {
            GameObject instance = Instantiate(fadingTextPrefab, transform.parent);
            TMP_Text text = instance.GetComponentInChildren<TMP_Text>();
            text.color = textColor;
            text.text = $"{value:0}";
            Destroy(instance, destroyTextAfter);
        }
    }
}
