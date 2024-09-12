using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientQueuePlace : MonoBehaviour
{
    public Action<ClientQueuePlace> OnPlaceFreed;
    public bool Istaken => _istaken;
    private bool _istaken;

    public void SetIsTaken(bool value)
    {
        _istaken = value;
        if(!_istaken)OnPlaceFreed?.Invoke(this);
    }
}
