using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class LevelTimeDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text _remainingTimeText;
    private float _startingTime;
    private float _remainingTime;
    private bool _countDown = false;
    private void Update()
    {
        //if (!_countDown) return;
        //_remainingTime-=(Time.deltaTime*PauseSettings.TimeSpeed);
        //_remainingTime = math.clamp(_remainingTime, 0, _startingTime);
        //_remainingTimeText.text = FormatTime();
        //if (_remainingTime <= 0) _countDown = false;
    }
    public void StartCoundown()
    {
        _countDown = true;
    }
    public void SetTime(float time)
    {
        _startingTime = time;
        _remainingTime = time;
        _remainingTimeText.text = FormatTime();
    }
    public void SetRemainingTime(float time)
    {
        _remainingTime = time;
        _remainingTimeText.text = FormatTime();
    }
    string FormatTime()
    {
        int minutes = (int)_remainingTime / 60;
        int seconds = (int)_remainingTime % 60;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
