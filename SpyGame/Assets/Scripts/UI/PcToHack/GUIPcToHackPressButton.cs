using UnityEngine;
using System.Collections;
using SpyGame;
using SpyGame.Events;
using System;

public class GUIPcToHackPressButton : MonoBehaviour
{
    [Header("Hack Available Events")]
    public SpyGame.Events.EventType _canHackEvent;
    public SpyGame.Events.EventType _cannotHackEvent;

    [Header("Hack Available Events")]
    public SpyGame.Events.EventType _onStartHackEvent;
    public SpyGame.Events.EventType _onFinishHackvent;

    [Header("Hack GUI Position")]
    public Vector3 _offset;
    public Vector3 _rotation;
    public float _scale;

    [Header("Hack GUI Position")]
    public Transform a;

    PCToHack _pcToHack;
    // Use this for initialization
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

        // set position for hacking bar


        // setting listeners
        SceneController.Game.EventManager.RegisterListener(_canHackEvent, onCanHackEvent);
        SceneController.Game.EventManager.RegisterListener(_cannotHackEvent, onCannotHackEvent);

        gameObject.SetActive(false);
    }


    private void onCanHackEvent(GameEvent e)
    {
        gameObject.SetActive(true);
    } // onCanHackEvent

    private void onCannotHackEvent(GameEvent e)
    {
        gameObject.SetActive(false);
    } // onCannotHackEvent

    private void onStartHackEvent(GameEvent e)
    {

    } // onStartHackEvent

    private void onFinishHackEvent(GameEvent e)
    {

    } // onFinishHackEvent


}
