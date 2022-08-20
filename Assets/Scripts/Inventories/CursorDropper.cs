using GameDevTV.Inventories;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Inventories
{
    public class CursorDropper : ItemDropper
    {
        protected override Vector3 GetDropLocation()
        {
            RaycastHit rayHit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out rayHit))
                return rayHit.point;
            return transform.position;
        }
    }
}