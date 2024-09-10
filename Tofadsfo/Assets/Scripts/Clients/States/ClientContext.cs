using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientContext
{
    public Action<ClientState> ChangeState;
    public ClientQueue queue;
    public ClientQueuePlace assigendPlaceInQueue;
    public Transform transform;
}
