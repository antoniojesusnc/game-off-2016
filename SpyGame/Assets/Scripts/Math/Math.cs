using UnityEngine;
using System.Collections;
using Geometry;

namespace Utils
{
    public class Math
    {

        /// <summary>
        /// Check if a Point is inside a Cone
        /// </summary>
        /// <param name="cone">
        /// The object Cone, </param>
        /// <param name="coneOrigin"> 
        /// The origin or the Cone </param>
        /// <param name="coneFacing">
        /// The front of the Cone </param>
        /// <param name="point">
        /// Point to check if is inside Cone</param>
        /// <returns>true if inside, false otherwise</returns>
        public static bool isInsideCone(Cone cone, Vector3 coneOrigin, Vector3 coneFacing, Vector3 point, bool ignoreY = true)
        {
            if (ignoreY)
                point.Set(point.x, coneOrigin.y, point.z);
            Vector3 directioPointToCone = ( point - coneOrigin );
            // if point if fardest than Cone range, false
            if (directioPointToCone.sqrMagnitude > cone.getLength() * cone.getLength())
                return false;

            /*
            Debug.Log("Dot "+Vector3.Dot(coneFacing.normalized, directioPointToCone.normalized));
            Debug.Log("Acos " + Mathf.Acos(Vector3.Dot(coneFacing.normalized, directioPointToCone.normalized)));
            Debug.Log("AcosRad " + Mathf.Rad2Deg* Mathf.Acos(Vector3.Dot(coneFacing.normalized, directioPointToCone.normalized)));
            */
            if (Mathf.Acos(Vector3.Dot(coneFacing.normalized, directioPointToCone.normalized)) * Mathf.Rad2Deg <= cone.getAngle() * 0.5f)
                return true;

            return false;
        } // isInsideCone

        public static float DistanceSqr(Vector3 v1, Vector3 v2)
        {
            return ( v2.x - v1.x ) * ( v2.x - v1.x ) + ( v2.y - v1.y ) * ( v2.y - v1.y );
        }

        public static float Distance(Vector3 v1, Vector3 v2)
        {
            return (float)System.Math.Sqrt(DistanceSqr(v1, v2));
        }

    } // Math
}