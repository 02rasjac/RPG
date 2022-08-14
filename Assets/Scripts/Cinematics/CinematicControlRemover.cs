using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        void OnEnable()
        {
            GetComponent<PlayableDirector>().played  += DisableControls;
            GetComponent<PlayableDirector>().stopped += EnableControls;
        }

        void OnDisable()
        {
            GetComponent<PlayableDirector>().played  -= DisableControls;
            GetComponent<PlayableDirector>().stopped -= EnableControls;
        }

        void DisableControls(PlayableDirector pd)
        {
            var player = GameObject.FindWithTag("Player");
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }
        
        void EnableControls(PlayableDirector pd)
        {
            var player = GameObject.FindWithTag("Player");
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
}