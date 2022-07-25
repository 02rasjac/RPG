using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            GetComponent<PlayableDirector>().played += DisableControls;
            GetComponent<PlayableDirector>().stopped += EnableControls;
        }

        void DisableControls(PlayableDirector pd)
        {
            print("Disable controls");
        }
        
        void EnableControls(PlayableDirector pd)
        {
            print("Enable controls");
        }
    }
}