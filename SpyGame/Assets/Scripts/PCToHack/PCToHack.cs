using UnityEngine;
using System.Collections;
using System;

//[RequireComponent(typeof(SphereCollider))]
public class PCToHack : MonoBehaviour
{

    public float _usableRange;
    private bool _canBeUsable;

    void Start()
    {
        Init();
    } // Start

    private void Init()
    {
        GetComponentInChildren<SphereCollider>().radius = _usableRange;
    } // Init

    public void OnTriggerEnter(Collider other)
    {
        _canBeUsable = true;
        // here i need to tell to the GUI the pc can Be hackeable
    } // OnTriggerEnter

    public void OnTriggerExit(Collider other)
    {
        _canBeUsable = false;
        // here i need to tell to the GUI the pc can NOT Be hackeable
    } // OnTriggerExit
}
