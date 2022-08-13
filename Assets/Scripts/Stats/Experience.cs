using System.Collections;
using UnityEngine;

using RPG.Saving;
using Newtonsoft.Json.Linq;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experience = 0f;

        /// <summary>
        /// Get how much experience this has.
        /// </summary>
        /// <returns>Ammount of experience</returns>
        public float GetExperience() => experience;

        /// <summary>
        /// Increase experience by <paramref name="ammount"/>.
        /// </summary>
        /// <param name="ammount">Ammount of experience to increase by.</param>
        public void GainExperience(float ammount)
        {
            experience += ammount;
            GetComponent<BaseStats>().UpdateLevel(experience);
        }

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