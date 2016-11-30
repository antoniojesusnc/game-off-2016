using UnityEngine;
using System.Collections;
using SpyGame.WorldObjects;
using System;

namespace SpyGame
{
    public class CameraShowDoor : MonoBehaviour
    {
        public float _maxAngleTorsion;
        public float _timeToReachDoor;
        public float _timeToReachPlayer;
        public float _timeWaiting;

        private Door _door;

        private Vector3 _initialPosition;
        private Vector3 _finalPosition;
        private float _timeStamp;

        private bool _enable;
        private bool _waiting;
        private bool _returning;

        public void Init()
        {
            _timeStamp = 0;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (!_enable)
                return;

            _timeStamp += Time.deltaTime;
            if (_waiting)
            {
                LateUpdateWaiting();
            } else
            {
                LateUpdateMoving();
            }
        } // LateUpdate

        private void LateUpdateWaiting()
        {
            if (_timeStamp > _timeWaiting)
            {
                ReturnToOriginalPosition();
            }
        } // LateUpdateWaiting

        private void LateUpdateMoving()
        {
            if (_returning)
            {
                transform.position = Vector3.Lerp(_initialPosition, _finalPosition, _timeStamp / _timeToReachPlayer);
            } else
            {
                transform.position = Vector3.Lerp(_initialPosition, _finalPosition, _timeStamp / _timeToReachDoor);
            }

            float time = _returning ? _timeToReachPlayer : _timeToReachDoor;
            if (_timeStamp > time)
            {
                if (!_returning)
                {
                    ReachDoorPosition();
                } else
                {
                    Debug.Log("camera reach player positoin");
                }
            }
        } // LateUpdateMoving

        private void ReachDoorPosition()
        {
            _waiting = true;
            _timeStamp = 0;
        } // ReachDoorPosition

        private void ReturnToOriginalPosition()
        {
            _timeStamp = 0;
            Vector3 temp = _initialPosition;
            _initialPosition = _finalPosition;
            _finalPosition = temp;
            _waiting = false;
            _returning = true;
        } // ReturnToOriginalPosition

        // gets & sets
        public float GetTotalTime()
        {
            return _timeToReachDoor + _timeWaiting + _timeToReachPlayer;
        } // GetTotalTime

        public void SetFinalPoint(Vector3 position)
        {
            _finalPosition = position;
            _initialPosition = transform.position;
        } // SetFinalPoint

        public void StartMovement()
        {
            _enable = true;
            _waiting = false;
            _returning = false;
        } // StartMovement

        public void SetEnable(bool enable)
        {
            _enable = enable;
            enabled = enabled;
        }
    }
}
