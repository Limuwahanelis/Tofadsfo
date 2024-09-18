using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientAtRegisterState : ClientState
{
    public static Type StateType { get => typeof(ClientAtRegisterState); }
    public ClientAtRegisterState(GetState function) : base(function)
    {
    }

    public override void Update()
    {

    }

    public override void SetUpState(ClientContext context)
    {
        base.SetUpState(context);
        _context.register.OnOrderDelivered += OrderDelivered;
        _context.register.PlaceOrder();
    }

    public override void InterruptState()
    {
     
    }
    private void OrderDelivered()
    {
        _context.register.OnOrderDelivered -= OrderDelivered;
        _context.register.TakeOrder();
        _context.queue.SetQueuePlace(_context.assigendPlaceInQueue, false);
        ChangeState(ClientGoToDoorState.StateType);
    }
}