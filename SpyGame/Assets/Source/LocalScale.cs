using UnityEngine;
using System.Collections;

public class LocalScale : MonoBehaviour
{
    public Vector3 _newlocalScale;

    public void SetLocalScale()
    {
        transform.localScale = _newlocalScale;
    } // SetLocalScale
}
