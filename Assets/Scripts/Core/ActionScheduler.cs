using System.Collections;
using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        MonoBehaviour currentAction;

        public void StartAction(MonoBehaviour action)
        {
            if (currentAction != null && currentAction != action)
                print("Cancelling " + currentAction);
            currentAction = action;
        }
    }
}