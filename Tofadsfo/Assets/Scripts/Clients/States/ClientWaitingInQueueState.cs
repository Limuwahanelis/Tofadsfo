using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientWaitingInQueueState : ClientState
{
    public static Type StateType { get => typeof(ClientWaitingInQueueState); }
    public ClientWaitingInQueueState(GetState function) : base(function)
    {
    }

    public override void Update()
    {

    }

    public override void SetUpState(ClientContext context)
    {
        base.SetUpState(context);

    }

    public override void InterruptState()
    {
     
    }
}