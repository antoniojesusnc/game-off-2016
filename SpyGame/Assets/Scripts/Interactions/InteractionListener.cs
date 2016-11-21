using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using SpyGame.Events;
using System;

namespace SpyGame.Interactions
{

    public class InteractionListener : MonoBehaviour
    {
        public List<InteractionPairData> _listeners;

        void Start()
        {
            Init();
        } // Start

        public void Init()
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                SpyGame.SceneController.Game.EventManager.RegisterListener(_listeners[i].eventListener, OnEventMethod);
            }
        } // Init

        private void OnEventMethod(GameEvent e)
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                if (_listeners[i].eventListener == ( e.EventType as Events.EventType ) &&
                        ( _listeners[i].source == null || _listeners[i].source == e.EventData ))
                    _listeners[i].interactionMethod.Invoke();
            }
        } // OnEventMethod
    }
}


