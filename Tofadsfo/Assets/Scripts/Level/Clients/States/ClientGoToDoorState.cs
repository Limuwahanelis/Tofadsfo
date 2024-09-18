using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ClientGoToDoorState : ClientState
{
    private Vector2 _startPos;
    private float _lerp = 0;
    private float _timetoReachTarget;
    private int _targetindex = 0;
    public static Type StateType { get => typeof(ClientGoToDoorState); }
    public ClientGoToDoorState(GetState function) : base(function)
    {
    }

    public override void Update()
    {
        _context.transform.position = Vector2.Lerp(_startPos, _context.door.position, _lerp / _timetoReachTarget);
        _lerp = _lerp + Time.deltaTime*PauseSettings.TimeSpeed;
        if (_lerp / _timetoReachTarget > 1)
        {

            ChangeState(ClientAtDoorState.StateType);
            return;
        }
    }

    public override void SetUpState(ClientContext context)
    {
        base.SetUpState(context);
        _lerp = 0;
        _targetindex = 0;
        _startPos = _context.transform.position;
        _timetoReachTarget = Vector2.Distance(_startPos, _context.door.position) / _context.speed;
    }

    public override void InterruptState()
    {
     
    }
}