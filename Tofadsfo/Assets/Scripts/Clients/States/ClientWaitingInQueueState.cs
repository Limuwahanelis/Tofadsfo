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
        _context.queue.OnPlaceFreed += CheckPlace;
    }

    public override void InterruptState()
    {
        _context.queue.OnPlaceFreed -= CheckPlace;
    }
    private void CheckPlace(ClientQueuePlace place)
    {
        if (_context.queue.GetPlaceIndex(place)==_context.queue.GetPlaceIndex(_context.assigendPlaceInQueue)-1)
        {
            ClientQueuePlace newPlace= _context.queue.GetFreePlace();
            _context.queue.SetQueuePlace(_context.assigendPlaceInQueue, false);
            _context.assigendPlaceInQueue = newPlace;
            ChangeState(ClientGoToAssignedPlaceInQueueState.StateType);
        }
    }
}