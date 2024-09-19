using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    public float ElapsedTime => _levelElapsedTime;
    [SerializeField] LevelTimeDisplay _levelTimeDisplay;
    private float _timeToEndLevel = 0;
    private float _remainingTime = 0;
    private float _levelElapsedTime= 0;
    private bool _countTime = false;

    // Update is called once per frame
    void Update()
    {
        if (_countTime)
        {
            _remainingTime -= Time.deltaTime * PauseSettings.TimeSpeed;
            _levelElapsedTime += Time.deltaTime * PauseSettings.TimeSpeed;
            _remainingTime = math.clamp(_remainingTime, 0, _timeToEndLevel);
            if (_remainingTime < 0)
            {
                _countTime = false;
            }
            _levelTimeDisplay.SetRemainingTime(_remainingTime);
        }
    }
    public void SetUp(float _levelTime)
    {
        _timeToEndLevel = _levelTime;
        _remainingTime = _timeToEndLevel;
        _levelElapsedTime = 0;
    }

    public void SetCountdown(bool value)
    {
        _countTime = value;
    }
}
