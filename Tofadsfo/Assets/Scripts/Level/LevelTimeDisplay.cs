using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class LevelTimeDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text _remainingTimeText;
    private float _remainingTime;
    private void Update()
    {

    }
    public void SetTime(float time)
    {
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
