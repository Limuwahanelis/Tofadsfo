using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClientGoToAssignedPlaceInQueueState : ClientState
{
    private Vector2 _startPos;
    private float _lerp = 0;
    private float _timetoReachTarget;
    public static Type StateType { get => typeof(ClientGoToAssignedPlaceInQueueState); }
    public ClientGoToAssignedPlaceInQueueState(GetState function) : base(function)
    {
    }

    public override void Update()
    {
        _context.transform.position = Vector2.Lerp(_startPos, _context.assigendPlaceInQueue.transform.position, _lerp / _timetoReachTarget);
        _lerp = _lerp + Time.deltaTime * PauseSettings.TimeSpeed;
        if (_lerp / _timetoReachTarget > 1)
        {
            _context.transform.position = _context.assigendPlaceInQueue.transform.position;
            if (_context.queue.GetPlaceIndex(_context.assigendPlaceInQueue) == 0) ChangeState(ClientAtRegisterState.StateType);
            else ChangeState(ClientWaitingInQueueState.StateType);
        }
    }

    public override void SetUpState(ClientContext context)
    {
        base.SetUpState(context);
        _lerp = 0;
        _startPos=_context.transform.position;
        _timetoReachTarget = Vector2.Distance(_startPos, _context.assigendPlaceInQueue.transform.position) / _context.speed;
        int index=_context.queue.GetPlaceIndex(_context.assigendPlaceInQueue);
        if (index == 0) return;
        _context.queue.GetPlaceAtIndex(index - 1).OnPlaceFreed += ChangePlace;
    }
    private void ChangePlace(ClientQueuePlace place)
    {
        place.OnPlaceFreed-=ChangePlace;
        _context.queue.SetQueuePlace(_context.assigendPlaceInQueue, false);
        _context.assigendPlaceInQueue = place;
        _context.queue.SetQueuePlace(place, true);
        ChangeState(StateType);

    }
    public override void InterruptState()
    {
        int index = _context.queue.GetPlaceIndex(_context.assigendPlaceInQueue)-1;
        if (index == -1) return;
        _context.queue.GetPlaceAtIndex(index).OnPlaceFreed -= ChangePlace;
    }
}