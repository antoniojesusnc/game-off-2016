using UnityEngine;
using System.Collections;
using System;
using SpyGame;
using SpyGame.Events;

//[RequireComponent(typeof(SphereCollider))]
public class EndPoint : MonoBehaviour
{
    [Header("World Poperties")]
    public float _usableRange;

    [Header("On Enter End Point Event")]
    public SpyGame.Events.EventType _onReachEndPointEvent;

    private bool _playerInside;

    void Start()
    {
        Init();
    } // Start

    private void Init()
    {
        GetComponentInChildren<SphereCollider>().radius = _usableRange;
    } // Init

    // Trigger Events
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            Debug.Log("Someting enter in PCToHack and it is not the Player");
            return;
        }
        SceneController.Game.EventManager.Emit(_onReachEndPointEvent, this);
        _playerInside = true;
        Debug.Log("Player Inside");
    } // OnTriggerEnter

    public void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
        {
            Debug.Log("Someting enter in PCToHack and it is not the Player");
            return;
        }

        _playerInside = false;
        Debug.Log("Player Outside");
    } // OnTriggerExit
}
