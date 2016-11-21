using UnityEngine;
using System.Collections;

public class DebugKeyToHack : MonoBehaviour
{

    public SpyGame.Events.EventType _eventStartHacking;

    bool _canHack;

    public void SetCanHack(bool canHack)
    {
        _canHack = canHack;
    }

    void Update()
    {
        if (!_canHack)
            return;

        if (Input.GetKey(KeyCode.Space))
        {
            StartHacking();
        }
    } // Update

    private void StartHacking()
    {
        SpyGame.SceneController.Game.EventManager.Emit(_eventStartHacking);
        gameObject.SetActive(false);
    } // StartHacking
}
