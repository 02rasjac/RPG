using UnityEngine;

namespace RPG.Core
{
    public interface IRaycastable
    {
        public CursorType GetCursorType();
        /// <summary>
        /// Component will handle a raycast if it implements <c>IRaycastable</c>.
        /// </summary>
        /// <param name="caller">The gameobject that calls this, mainly <c>PlayerController</c>.</param>
        /// <param name="isPressed">If action-button is pressed.</param>
        /// <returns><c>true</c> if the raycast was handled, otherwise <c>false</c>.</returns>
        public bool HandleRaycast(GameObject caller, bool isPressed);
    }
}
