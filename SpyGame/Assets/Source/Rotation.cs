using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour
{
    public Vector3 _rotation;

    public void SetRotation()
    {
        transform.rotation = Quaternion.Euler(_rotation);
    }
}
