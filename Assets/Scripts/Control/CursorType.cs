using UnityEngine;

namespace RPG.Control
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "NewCursorType", menuName = "New Cursor Type")]
    public class CursorType : ScriptableObject
    {
        public Texture2D texture;
        [Tooltip("For basic: (10, 4). \nFor bonus: (5, 2).")]
        public Vector2 hotspot = new Vector2(5, 2);

        public void SetAsCursor()
        {
            Cursor.SetCursor(texture, hotspot, CursorMode.Auto);
        }
    }
}