using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientWaitingInQueueState : ClientState
{
    public static Type StateType { get => typeof(ClientWaitingInQueueState); }
    private bool _wasAssignedNewPos = false;
    public ClientWaitingInQueueState(GetState function) : base(function)
    {
    }

    public override void Update()
    {

    }
    public override void SetUpState(ClientContext context)
    {
        base.SetUpState(context);
        _wasAssignedNewPos = false;
        //_context.queue.OnPlaceFreed += CheckPlace;
        int index = _context.queue.GetPlaceIndex(_context.assigendPlaceInQueue);
        if (index == 0) return;
        _context.queue.GetPlaceAtIndex(index - 1).OnPlaceFreed += ChangePlace;
    }
    private void ChangePlace(ClientQueuePlace place)
    {
        place.OnPlaceFreed -= ChangePlace;
        _context.queue.SetQueuePlace(_context.assigendPlaceInQueue, false);
        _context.assigendPlaceInQueue = place;
        _context.queue.SetQueuePlace(place, true);
        ChangeState(ClientGoToAssignedPlaceInQueueState.StateType);

    }
    public override void InterruptState()
    {
        int index = _context.queue.GetPlaceIndex(_context.assigendPlaceInQueue) - 1;
        if (index == -1) return;
        _context.queue.GetPlaceAtIndex(index).OnPlaceFreed -= ChangePlace;
    }
    //private void CheckPlace(ClientQueuePlace place)
    //{
    //    if (_wasAssignedNewPos) return;
    //    if (_context.queue.GetPlaceIndex(place)==_context.queue.GetPlaceIndex(_context.assigendPlaceInQueue)-1)
    //    {
    //        _context.queue.OnPlaceFreed -= CheckPlace;
    //        _wasAssignedNewPos = true;
    //        ClientQueuePlace newPlace= _context.queue.GetFreePlace();
    //        ClientQueuePlace tmp = _context.assigendPlaceInQueue;
    //        _context.assigendPlaceInQueue = newPlace;
    //        _context.queue.SetQueuePlace(tmp, false);
    //        ChangeState(ClientGoToAssignedPlaceInQueueState.StateType);
    //    }
    //}
}