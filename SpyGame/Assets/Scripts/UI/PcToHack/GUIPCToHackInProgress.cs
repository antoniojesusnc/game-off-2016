using UnityEngine;
using System.Collections;
using SpyGame;
using SpyGame.Events;
using System;

public class GUIPCToHackInProgress : MonoBehaviour
{
    [Header("GUI Position")]
    public Vector3 _offset;
    public Vector3 _rotation;
    public float _scale;


    private PCToHack _pcToHack;

    void Start()
    {
        Init();
    }

    void Init()
    {
        _pcToHack = GameObject.FindWithTag("PcToHack").GetComponent<PCToHack>();

        // set position and rotation to button to press
        Transform pcToHack = _pcToHack.transform;
        transform.position = pcToHack.position + _offset;
        transform.rotation = Quaternion.Euler(_rotation);
        transform.localScale = Vector3.one * _scale;

        gameObject.SetActive(false);
    } // Init
}
