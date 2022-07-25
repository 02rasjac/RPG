using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool beenTriggered = false;

        void OnTriggerEnter(Collider other)
        {
            if (!beenTriggered && other.CompareTag("Player"))
            {
                GetComponent<PlayableDirector>().Play();
                beenTriggered = true;
            }
        }
    }
}
