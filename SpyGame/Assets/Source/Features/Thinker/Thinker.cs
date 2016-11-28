using SpyGame;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Thinker : MonoBehaviour
{
    public Brain brain;

    public void Start()
    {
        brain.Initialize(this);
    }

    void FixedUpdate()
    {
        brain.Think(this);
    }
}