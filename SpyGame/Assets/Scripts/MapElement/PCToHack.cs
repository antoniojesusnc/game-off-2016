using UnityEngine;
using System.Collections;
using System;
using SpyGame;

//[RequireComponent(typeof(SphereCollider))]
public class PCToHack : MonoBehaviour
{
    public float _usableRange;

    public EventGeneric _canHackEvent;
    public EventGeneric _canontHackEvent;
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
        SceneController.Game.EventManager.Emit(_canHackEvent, this);
        _canBeUsable = true;
        // here i need to tell to the GUI the pc can Be hackeable
    } // OnTriggerEnter

    public void OnTriggerExit(Collider other)
    {
        SceneController.Game.EventManager.Emit(_canontHackEvent, this);
        _canBeUsable = false;
        // here i need to tell to the GUI the pc can NOT Be hackeable
    } // OnTriggerExit
}
