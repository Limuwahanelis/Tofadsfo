using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSpeedControl : MonoBehaviour
{
    public void SetTimeSpeed(float value)
    {
        PauseSettings.SetTimeSpeed(value);
    }
}
