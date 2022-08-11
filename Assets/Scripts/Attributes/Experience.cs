using System.Collections;
using UnityEngine;

namespace RPG.Attributes
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] float experience = 0f;

        public void GainExperience(float ammount) => experience += ammount;
    }
}