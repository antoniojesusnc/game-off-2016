using UnityEngine;
using System.Collections;
using System;
using SpyGame;
using SpyGame.Events;

//[RequireComponent(typeof(SphereCollider))]
public class PCToHack : MonoBehaviour
{
    [Header("Hack Properties")]
    public float _hackTime;

    [Header("World Poperties")]
    public float _usableRange;

    [Header("Hack Available Events")]
    public SpyGame.Events.EventType _canHackEvent;
    public SpyGame.Events.EventType _canontHackEvent;

    [Header("Hacking Event")]
    public SpyGame.Events.EventType _onStartHackingEvent;
    public SpyGame.Events.EventType _onFinishHackingEvent;

    private bool _isHacking = false;
    private float _timeStamp;
    private bool _canBeUsable;

    void Start()
    {
        Init();
    } // Start

    private void Init()
    {
        GetComponentInChildren<SphereCollider>().radius = _usableRange;

        SpyGame.SceneController.Game.EventManager.RegisterListener(_onStartHackingEvent, OnStartHacking);
    } // Init

    public void OnStartHacking(GameEvent e)
    {
        _isHacking = true;
        //SceneController.Game.EventManager.Emit(_onStartHackingEvent, this);
    } // OnStartHacking

    public void OnFinishHacking()
    {
        _isHacking = false;
        SceneController.Game.EventManager.Emit(_onFinishHackingEvent, this);
    } // OnFinishHacking

    void Update()
    {
        if (!_isHacking)
            return;

        _timeStamp += Time.deltaTime;

        if (_timeStamp > _hackTime)
            OnFinishHacking();
    } // Update

    // Trigger Events
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

    // gets & sets

    public float GetPercentageHacked()
    {
        return _timeStamp / _hackTime;
    } // GetHackTime
}
