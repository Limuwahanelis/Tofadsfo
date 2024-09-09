using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class WorkerGoToProductState : WorkerState
{
    private int _currentPositionIndex = 0;
    private Vector2 _startPos;
    private float _lerp = 0;
    private float _timetoReachTarget;
    private float _speed = 2;
    public static Type StateType { get => typeof(WorkerGoToProductState); }
    public WorkerGoToProductState(GetState function) : base(function)
    {
    }

    public override void Update()
    {
        _workerContext.workerTran.position = Vector2.Lerp(_startPos,_workerContext.currentPath[_currentPositionIndex], _lerp / _timetoReachTarget);
        _lerp = _lerp + Time.deltaTime;
        if (_lerp / _timetoReachTarget > 1)
        {
            if (_currentPositionIndex >= _workerContext.currentPath.Count - 1)
            {
                _currentPositionIndex = 0;
                _workerContext.workerTran.position = ((Vector3Int)_workerContext.currentPath[_currentPositionIndex]);
                //_startPos = _positionsToVisit[0];
            }
            _startPos = _workerContext.currentPath[_currentPositionIndex];
            _currentPositionIndex++;
            _timetoReachTarget = Vector2.Distance(_startPos, _workerContext.currentPath[_currentPositionIndex]) / _speed;
            _lerp = 0;
        }
    }

    public override void SetUpState(WorkerContext context)
    {
        base.SetUpState(context);
        _startPos = _workerContext.currentPath[0];
        _timetoReachTarget = Vector2.Distance(_startPos, _workerContext.currentPath[0]) / _speed;
    }

    public override void InterruptState()
    {
     
    }
}