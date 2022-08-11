using System.Collections;
using UnityEngine;

using RPG.Saving;
using Newtonsoft.Json.Linq;

namespace RPG.Attributes
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experience = 0f;

        public void GainExperience(float ammount) => experience += ammount;

        public JToken CaptureAsJToken()
        {
            return JToken.FromObject(experience);
        }

        public void RestoreFromJToken(JToken state)
        {
            experience = state.ToObject<float>();
        }
    }
}