using SpyGame;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Thinker : MonoBehaviour
{
    public Brain brain;
    private bool _canThink;

    public void Start()
    {
        _canThink = true;
        brain.Initialize(this);
    }

    void FixedUpdate()
    {
        if (!_canThink)
            return;

        brain.Think(this);
    }

    public void SetCanThink(bool canThink)
    {
        _canThink = canThink;
        if (!canThink)
            brain.Stop(this);
    }
}