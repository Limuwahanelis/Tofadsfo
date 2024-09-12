using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorkerGoToAssemblerState : WorkerState
{
    private int _currentPositionIndex = 0;
    private Vector2 _startPos;
    private float _lerp = 0;
    private float _timetoReachTarget;
    public static Type StateType { get => typeof(WorkerGoToAssemblerState); }
    public WorkerGoToAssemblerState(GetState function) : base(function)
    {
    }

    public override void Update()
    {
        _context.workerTran.position = Vector2.Lerp(_startPos, _context.currentPath[_currentPositionIndex], _lerp / _timetoReachTarget);
        _lerp = _lerp + Time.deltaTime;
        if (_lerp / _timetoReachTarget > 1)
        {
            if (_currentPositionIndex >= _context.currentPath.Count - 1)
            {
                _context.workerTran.position = ((Vector3Int)_context.currentPath[_currentPositionIndex]);
                ChangeState(WorkerAtAssemblerState.StateType);
                return;
            }
            _startPos = _context.currentPath[_currentPositionIndex];
            _currentPositionIndex++;
            _timetoReachTarget = Vector2.Distance(_startPos, _context.currentPath[_currentPositionIndex]) / _context.speed;
            _lerp = 0;
        }
    }

    public override void SetUpState(WorkerContext context)
    {
        base.SetUpState(context);
        _currentPositionIndex = 0;
        _startPos = _context.currentPath[0];
        _timetoReachTarget = Vector2.Distance(_startPos, _context.currentPath[1]) / _context.speed;
    }

    public override void InterruptState()
    {
     
    }
}