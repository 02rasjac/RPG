using UnityEngine;

namespace RPG.Control
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "NewCursorType", menuName = "New Cursor Type")]
    public class CursorType : ScriptableObject
    {
        public Texture2D texture;
        public Vector2 hotspot;

        public void SetCursor()
        {
            Cursor.SetCursor(texture, hotspot, CursorMode.Auto);
        }
    }
}