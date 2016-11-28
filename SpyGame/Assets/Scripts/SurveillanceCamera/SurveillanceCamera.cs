using SpyGame;
using UnityEngine;
using System.Collections;
/*
namespace SpyGame
{
*/
public class SurveillanceCamera : MonoBehaviour
{
    [Header("Movement Range")]
    public float _openAngle;

    [Header("Movement Duration")]
    public float _movementDuration;

    [Header("Waiting Duration")]
    public float _waitingDuration;

    private Quaternion _rotationOrigin;
    private Quaternion _rotationDestiny;
    private bool _isWaiting;
    private float _timeStamp;

    bool _enable;

    void Start()
    {
        StartMovement();
    }

    public void StartMovement()
    {
        _rotationOrigin = transform.rotation * Quaternion.Euler(0, -_openAngle * 0.5f, 0);
        _rotationDestiny = transform.rotation * Quaternion.Euler(0, _openAngle * 0.5f, 0);
        _isWaiting = false;
        _timeStamp = 0;
        _enable = true;
    } // StartMovement

    void Update()
    {
        if (!_enable)
            return;

        if (_isWaiting)
        {
            UpdateWaiting();
        } else
        {
            UpdateRotation();
        }
    } // Update

    private void UpdateWaiting()
    {
        _timeStamp += Time.deltaTime;
        if (_timeStamp > _waitingDuration)
        {
            _isWaiting = false;
            _timeStamp = 0;
            Quaternion temp = _rotationOrigin;
            _rotationOrigin = _rotationDestiny;
            _rotationDestiny = temp;
        }
    } // UpdateWaiting

    private void UpdateRotation()
    {
        _timeStamp += Time.deltaTime;
        transform.rotation = Quaternion.Lerp(_rotationOrigin, _rotationDestiny, _timeStamp / _movementDuration);
        if (_timeStamp > _movementDuration)
        {
            _isWaiting = true;
            _timeStamp = 0;
        }
    } // UpdateRotation

    public void SetActiveCamera(bool active)
    {
        _enable = active;
    } // DeactivateCamera

    // gets & sets
    public float GetMaxRange()
    {
        return _openAngle;
    } // GetMaxRange

    public float GetMovementDuration()
    {
        return _movementDuration;
    } // GetMovementDuration

    public float GetWaitingDuration()
    {
        return _waitingDuration;
    } // GetMovementDuration

    public float GetTimeStamp()
    {
        return _timeStamp;
    } // GetTimeStamp
}
//}
