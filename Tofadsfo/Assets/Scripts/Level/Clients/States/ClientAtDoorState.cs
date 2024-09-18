using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientAtDoorState : ClientState
{
    public static Type StateType { get => typeof(ClientAtDoorState); }
    public ClientAtDoorState(GetState function) : base(function)
    {
    }

    public override void Update()
    {

    }

    public override void SetUpState(ClientContext context)
    {
        base.SetUpState(context);
        _context.ServeClient();
        _context.transform.position = _context.door.position;
    }

    public override void InterruptState()
    {
     
    }
}