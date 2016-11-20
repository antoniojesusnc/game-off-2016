using UnityEngine;
using System.Collections;
using SpyGame;
using SpyGame.Events;
using System;

public class GUIPcToHack : MonoBehaviour
{
    public EventGeneric _canHackEvent;
    public EventGeneric _cannotHackEvent;

    public Vector3 _offset;
    public Vector3 _rotation;
    public float _scale;

    // Use this for initialization
    void Start()
    {
        Transform pcToHack = GameObject.FindWithTag("PcToHack").transform;
        transform.position = pcToHack.position + _offset;
        transform.rotation = Quaternion.Euler(_rotation);
        transform.localScale = Vector3.one * _scale;

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
}
