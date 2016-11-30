using UnityEngine;
using System.Collections;

namespace SpyGame
{
    public class CameraController : MonoBehaviour
    {

        public CameraFollowPlayer _cameraFollowPlayer;
        public CameraShowDoor _cameraShowDoor;

        bool _followingPlayer;

        public float _timeStamp;

        void Awake()
        {
            _followingPlayer = true;
            _cameraFollowPlayer.Init();
            _cameraShowDoor.Init();
        } // Awake

        // Update is called once per frame
        void Update()
        {
            if (_followingPlayer)
                return;

            _timeStamp += Time.deltaTime;
            if (_timeStamp > _cameraShowDoor.GetTotalTime())
            {
                FollowPlayer();
            }
        } // Update

        public void ShowTheDoor()
        {
            _followingPlayer = false;
            _cameraFollowPlayer.SetEnable(false);
            _cameraShowDoor.SetEnable(true);
            _timeStamp = 0;

            Vector3 doorPosition = GameObject.FindObjectOfType<WorldObjects.Door>().transform.position;
            _cameraShowDoor.SetFinalPoint(_cameraFollowPlayer.CalulateStandardCameraPosition(doorPosition));
            _cameraShowDoor.StartMovement();
        } // ShowTheDoor

        public void FollowPlayer()
        {
            _followingPlayer = true;
            _cameraFollowPlayer.SetEnable(true);
            _cameraShowDoor.SetEnable(false);
        }
    }
}
