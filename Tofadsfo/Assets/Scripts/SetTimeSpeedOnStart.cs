using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTimeSpeedOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PauseSettings.SetTimeSpeed(1);
    }
}
