using UnityEngine;

namespace Assets.Models.Dto
{
    public class Vector3Dto
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public static implicit operator Vector3Dto(Vector3 vector)
        {
            return new Vector3Dto { X = vector.x, Y = vector.y, Z = vector.z };
        }

        public static implicit operator Vector3(Vector3Dto vector)
        {
            return new Vector3(vector.X, vector.Y, vector.Z);
        }
    }
}