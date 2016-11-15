using UnityEngine;
using System.Collections;
using SpyGame;
using SpyGame.Events;

public class bloop : MonoBehaviour {

	Game game;

	// Use this for initialization
	void Start () 
	{
		game = SceneController.Game;

		game.EventManager.RegisterListener (TestEvents.TEST01, OnTestEvent01);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTestEvent01(GameEvent e)
	{
		Debug.Log ("Got notified for event TEST01");
	}
}
