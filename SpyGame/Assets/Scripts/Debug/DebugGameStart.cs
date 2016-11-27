using UnityEngine;
using System.Collections;

public class DebugGameStart : MonoBehaviour
{
    public SpyGame.Events.EventType _startEvent;

    // Use this for initialization
    void Start()
    {
        SpyGame.SceneController.Game.EventManager.Emit(_startEvent);
    }
}
