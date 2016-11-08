using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour
{

    /// <summary>
    /// The camera dont move until the player go to the viewportborder, 
    /// when this happen the camera will move until the player in in the middle scren again;
    /// 
    /// The default Pos is some angle and distance from the player
    /// </summary>
    
    [Header("Camera Standard Properties")]
    public float _cameraAngle;
    public float _cameraDefaultDistance;
    public Vector3 _cameraOffset;

    [Header("Camera Configuration")]
    public float _timeToSetInMiddle;

    [Range(0, 1)]
    public float _viewportBorderX;
    [Range(0, 1)]
    public float _viewportBorderY;

    private Transform _mainPlayer;
    private Camera _camera;
    private Vector3 _viewportPlayerPos;

    private float _timeStamp;
    private bool _settingPlayerToMiddle = false;
    private Vector3 _originalCameraPosition;
    private Vector3 _destinyCameraPosition;

    // Use this for initialization
    void Start()
    {
        _mainPlayer = GameObject.FindWithTag("Player").transform;

        _camera = Camera.main;

        _camera.transform.rotation = CalulateStandardCameraRotation();
        _camera.transform.position = CalulateStandardCameraPosition();
    } // Start

    public Quaternion CalulateStandardCameraRotation()
    {
        return Quaternion.Euler(_cameraAngle, 0, 0);
    } // CalulateStandardCameraRotation

    public Vector3 CalulateStandardCameraPosition()
    {
        Vector3 result = _mainPlayer.transform.position - _camera.transform.forward * _cameraDefaultDistance;
        result += _cameraOffset;

        return result;
    } // setInitialPosition

    void LateUpdate()
    {
        _viewportPlayerPos = _camera.WorldToViewportPoint(_mainPlayer.position);

        if (IsSettingPlayerToTheMiddle())
        {
            UpdateSettingPlayerToMiddle(Time.deltaTime);
        } else
        {
            UpdateCheckBordersViewport(Time.deltaTime);
        }
    } // LateUpdate 

    private void UpdateSettingPlayerToMiddle(float dt)
    {
        _timeStamp += Time.deltaTime;

        _destinyCameraPosition = CalulateStandardCameraPosition();
        _camera.transform.position = Vector3.Lerp(_originalCameraPosition, _destinyCameraPosition, _timeStamp / _timeToSetInMiddle);

        if (_timeStamp >= _timeToSetInMiddle)
        {
            SetSettingPlayerToTheMiddle(false);
        }
    } // UpdateSettingPlayerToMiddle

    private void UpdateCheckBordersViewport(float dt)
    {
        if (_viewportPlayerPos.x < _viewportBorderX || _viewportPlayerPos.x > 1-_viewportBorderX ||
                _viewportPlayerPos.y < _viewportBorderY || _viewportPlayerPos.y > 1-_viewportBorderY)
        {
            _originalCameraPosition = _camera.transform.position;
            _timeStamp = 0;
            SetSettingPlayerToTheMiddle(true);
        }
    } // UpdateCheckBordersViewport

    public bool IsSettingPlayerToTheMiddle()
    {
        return _settingPlayerToMiddle;
    } // IsSettingPlayerToTheMiddle

    public void SetSettingPlayerToTheMiddle(bool setting)
    {
        _settingPlayerToMiddle = setting;
    } // SetSettingPlayerToTheMiddle
}
