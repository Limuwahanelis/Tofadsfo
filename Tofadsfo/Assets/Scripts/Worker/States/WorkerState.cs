using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorkerState
{
    public delegate WorkerState GetState(Type state);
    protected GetState _getType;
    protected WorkerContext _context;

    public WorkerState(GetState function)
    {
        _getType = function;
    }
    public virtual void SetUpState(WorkerContext context)
    {
        _context = context;
    }
    public virtual void Test() { }
    public abstract void Update();
    public abstract void InterruptState();
    public void ChangeState(Type newStateType)
    {
        WorkerState state = _getType(newStateType);
        _context.ChangeState(state);
        state.SetUpState(_context);
    }


}
