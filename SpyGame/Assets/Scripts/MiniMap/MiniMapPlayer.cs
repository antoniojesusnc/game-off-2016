using UnityEngine;
using System.Collections;

namespace SpyGame.MiniMap
{
    public class MiniMapPlayer : MonoBehaviour
    {
        private Transform _player;

        void Start()
        {
            _player = GameObject.FindWithTag("Player").transform;
        } // Start

        void Update()
        {
            transform.position = _player.position;
        } // Update
    } // MiniMapPlayer
}
