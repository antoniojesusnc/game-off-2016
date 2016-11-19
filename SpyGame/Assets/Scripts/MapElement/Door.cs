using UnityEngine;
using System.Collections;
using SpyGame;
using SpyGame.Events;

namespace SpyGame.WorldObjects
{
    public class Door : MonoBehaviour
    {

        public EventGeneric _eventToOpenDoor;
        public Transform _rotationPivot;

        public float _timeOpenDoor;
        public float _openAngle;

        private float _timeStamp;

        private bool _opening;
        private bool _opened;


        void Start()
        {
            SceneController.Game.EventManager.RegisterListener(_eventToOpenDoor, StartOpenDoor);
        } // Start

        /// <summary>
        /// Method call when event happend
        /// </summary>
        /// <param name="e"></param>
        public void StartOpenDoor(GameEvent e)
        {
            if (_opening)
                return;

            _opening = true;
        } // OpenDoor

        /// <summary>
        /// Method call when finish opening door
        /// </summary>
        private void FinishOpenDoor()
        {
            _opened = true;
            _opening = false;

            // adjusting final rotation just in case we lost something with the timeStamp;
            float offsetAngle = transform.localRotation.eulerAngles.y - _openAngle;
            transform.RotateAround(_rotationPivot.position, Vector3.up, -offsetAngle);
        } // FinishOpenDoor

        void Update()
        {
            if (_opened || !_opening)
                return;

            _timeStamp += Time.deltaTime;

            float angleOpen = ( _openAngle / _timeOpenDoor ) * Time.deltaTime;
            transform.RotateAround(_rotationPivot.position, Vector3.up, angleOpen);

            if (_timeStamp > _timeOpenDoor)
                FinishOpenDoor();
        } // Update
    }
}
