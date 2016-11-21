using UnityEngine;
using System.Collections;
using SpyGame;
using SpyGame.Events;
using System;

public class GUIPCToHackInProgress : MonoBehaviour
{
    [Header("Progress Bar Image")]
    public UnityEngine.UI.Image _progressBar;
    // private vars
    private bool _hackFinished;
    private bool _hackInProgress;

    private PCToHack _pcToHack;

    void Start()
    {
        Init();
    }

    void Init()
    {
        _pcToHack = GameObject.FindWithTag("PcToHack").GetComponent<PCToHack>();
        _hackFinished = false;
    } // Init

    public void StartHacking()
    {
        _hackInProgress = true;
        _progressBar.fillAmount = 0;
    } // StartHacking

    void Update()
    {
        UpdateHacking();
    } // Update

    public void UpdateHacking()
    {
        if (_hackFinished || !_hackInProgress)
            return;

        _progressBar.fillAmount = _pcToHack.GetPercentageHacked();
    } // UpdateHacking

    public void FinishHacking()
    {
        _hackInProgress = false;
        _hackFinished = true;
    } // FinishHacking
}
