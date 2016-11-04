using UnityEngine;
using System.Collections;
namespace Geometry
{
    [System.SerializableAttribute]
    public class Cone
    {
        [SerializeField]
        private float _angle;

        [SerializeField]
        private float _length;

        public Cone(float angle, float length)
        {
            _angle = angle;
            _length = length;
        } // Cone

        public float getAngle()
        {
            return _angle;
        } // getAngle

        public void setAngle(float angle)
        {
            _angle = angle;
        } // setAngle

        public void setLength(float length)
        {
            _length = length;
        } // setLength

        public float getLength()
        {
            return _length;
        } // getLength

    }
}