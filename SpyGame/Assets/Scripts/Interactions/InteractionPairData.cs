using UnityEngine;
using System.Collections;
using UnityEngine.Events;


namespace SpyGame.Interactions
{

    [System.Serializable]
    public class InteractionPairData
    {
        public string Name;
        public SpyGame.Events.EventType eventListener;
        public MonoBehaviour source;
        public UnityEvent interactionMethod;


    }
}
