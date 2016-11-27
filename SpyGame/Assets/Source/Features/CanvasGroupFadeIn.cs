using UnityEngine;
using System.Collections;

public class CanvasGroupFadeIn : MonoBehaviour
{
    [Header("Time")]
    public float _duration;

    private float _timeStamp;
    CanvasGroup _canvasGroup;
    bool _fading;

    public void CanvaGroupFadeIn()
    {
        if (_fading)
            return;

        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        _timeStamp = 0;
        _fading = true;
    } // CanvasGroupFadeIn

    void Update()
    {
        if (!_fading)
            return;

        if (_timeStamp < _duration)
        {
            _timeStamp += Time.deltaTime;
            _canvasGroup.alpha = _timeStamp / _duration;
            /*
            if (_timeStamp > _duration)
                _fading = false;
                */
        }
    } // Update
}
