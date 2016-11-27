using UnityEngine;
using System.Collections;

namespace SpyGame
{

    public class LevelController : MonoBehaviour
    {
        public SpyGame.Events.EventType _startLevelEvent;
        public SpyGame.Events.EventType _gameOverEvent;

        // Use this for initialization
        void Start()
        {
            SpyGame.SceneController.Game.EventManager.Emit(_startLevelEvent);
        }

        public void OnGameOver()
        {
            SpyGame.SceneController.Game.EventManager.Emit(_gameOverEvent);
        }
    }
}
