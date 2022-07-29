using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Saving
{
    [System.Serializable]
    internal class SerializableVector3
    {
        float x, y, z;

        public SerializableVector3(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        } 

        public Vector3 ToVector3()
        {
            return new Vector3(x, y, z);
        }
    }
}
