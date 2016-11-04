using UnityEngine;
using System.Collections;
using Geometry;

public class DebugCone : MonoBehaviour
{

    public Cone _cone;

    public Transform _objetive;

    bool check = false;

    void Update()
    {

        if ( ( Mathf.FloorToInt(Time.timeSinceLevelLoad % 2) == 0 ) ){
            if (!check ||  Input.GetKeyUp(KeyCode.A))
        {
                check = true;
            if (Utils.Math.isInsideCone(_cone, transform.position, transform.forward, _objetive.position))
                Debug.Log("inside");
            else
                Debug.Log("outside");
        }
        } else
        {
            check = false;
        }

    }

    public void OnDrawGizmos()
    {
        float halfAngle = _cone.getAngle() * 0.5f;
        for (float i = -halfAngle; i <= halfAngle; ++i)
        {
        //Gizmos.DrawLine(transform.position, transform.position+transform.forward * _cone.getLength());
            Vector3 dir = Quaternion.Euler(0, i, 0) * transform.forward;
            Gizmos.DrawLine(transform.position, transform.position+dir *_cone.getLength());
        }
    }
}
