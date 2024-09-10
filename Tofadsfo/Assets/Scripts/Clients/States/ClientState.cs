using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClientState
{
    public delegate ClientState GetState(Type state);
    protected GetState _getType;
    protected ClientContext _context;

    public ClientState(GetState function)
    {
        _getType = function;
    }
    public virtual void SetUpState(ClientContext context)
    {
        _context = context;
    }
    public virtual void Test() { }
    public abstract void Update();
    public abstract void InterruptState();
    public void ChangeState(Type newStateType)
    {
        ClientState state = _getType(newStateType);
        _context.ChangeState(state);
        state.SetUpState(_context);
    }

}
