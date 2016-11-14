using UnityEngine;
using System.Collections;
using System;

public class CameraMiniMap : MonoBehaviour {

    public float _ortographicDistance;
    public float _realDistance;

    private Transform _player;
    private Vector3 _playerOffset;
    // Use this for initialization
    void Start () {
        _player = GameObject.FindWithTag("Player").transform;

        SetInitialPosition();
    } // _player

    private void SetInitialPosition()
    {
        Camera cameraComponent = gameObject.GetComponent<Camera>();
        cameraComponent.orthographicSize = _ortographicDistance;

        cameraComponent.farClipPlane = 20;

        _playerOffset = Vector3.up * _realDistance;
        transform.position = _player.position + _playerOffset;
        transform.rotation = Quaternion.Euler(90, 0, 0);

    } // SetInitialPosition

    // Update is called once per frame
    void LateUpdate () {
        transform.position = _player.position + _playerOffset;
    } // LateUpdate
}
