using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnSceneStart : MonoBehaviour
{
    public UnityEvent OnSceneStarted;
    // Start is called before the first frame update
    void Start()
    {
        OnSceneStarted?.Invoke();
    }
}
