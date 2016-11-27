using UnityEngine;
using System.Collections;

namespace SpyGame
{

    public class LevelController : MonoBehaviour
    {
        public SpyGame.Events.EventType _startLevelEvent;
        public SpyGame.Events.EventType _gameFinishSuccess;
        public SpyGame.Events.EventType _gameOverEvent;


        public bool _enemyDetected;
        public bool _levelComplete;
        // Use this for initialization
        void Start()
        {
            SpyGame.SceneController.Game.EventManager.Emit(_startLevelEvent);
        }

        public void GameFinishSuccess()
        {
            SpyGame.SceneController.Game.EventManager.Emit(_gameFinishSuccess);
            _levelComplete = true;
        }

        public void GameOver()
        {
            _enemyDetected = true;
            SpyGame.SceneController.Game.EventManager.Emit(_gameOverEvent);
        }
    }
}
