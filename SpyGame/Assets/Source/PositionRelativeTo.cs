using UnityEngine;
using System.Collections;

public class PositionRelativeTo : MonoBehaviour
{

    public Transform _relativePosition;
    public Vector3 _offset;

    public void SetPosition()
    {
        transform.position = _relativePosition.position + _offset;
    }
}
