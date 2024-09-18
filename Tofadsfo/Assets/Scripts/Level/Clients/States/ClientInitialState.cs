using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientInitialState : ClientState
{
    public static Type StateType { get => typeof(ClientInitialState); }
    public ClientInitialState(GetState function) : base(function)
    {
    }

    public override void Update()
    {

    }

    public override void SetUpState(ClientContext context)
    {
        base.SetUpState(context);
        _context.assigendPlaceInQueue= _context.queue.GetFreePlace();
        ChangeState(ClientGoToAssignedPlaceInQueueState.StateType);
    }

    public override void InterruptState()
    {
     
    }
}