using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioRandomizer : MonoBehaviour
    {
        [SerializeField] AudioClip[] audioClips;
        [SerializeField] Vector2 pitchRange = new Vector2(1f, 1f);
        
        AudioSource audioSource;
        int lastIndex;

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayRandom()
        {
            int newIndex = 0;
            do {
                newIndex = Random.Range(0, audioClips.Length);
            } while (newIndex == lastIndex);

            lastIndex = newIndex;
            audioSource.clip = audioClips[newIndex];
            audioSource.pitch = Random.Range(pitchRange.x, pitchRange.y);
            audioSource.Play();
        }
    }
}
