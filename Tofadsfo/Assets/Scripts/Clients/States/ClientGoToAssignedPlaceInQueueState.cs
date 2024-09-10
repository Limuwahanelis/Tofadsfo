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
    private float _speed = 2;
    public static Type StateType { get => typeof(ClientGoToAssignedPlaceInQueueState); }
    public ClientGoToAssignedPlaceInQueueState(GetState function) : base(function)
    {
    }

    public override void Update()
    {
        _context.transform.position = Vector2.Lerp(_startPos, _context.assigendPlaceInQueue.transform.position, _lerp / _timetoReachTarget);
        _lerp = _lerp + Time.deltaTime;
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
        _timetoReachTarget = Vector2.Distance(_startPos, _context.assigendPlaceInQueue.transform.position) / _speed;
    }

    public override void InterruptState()
    {
     
    }
}