using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerAtProductTableState : WorkerState
{
    public static Type StateType { get => typeof(WorkerAtProductTableState); }
    public WorkerAtProductTableState(GetState function) : base(function)
    {
    }

    public override void Update()
    {

    }

    public override void SetUpState(WorkerContext context)
    {
        base.SetUpState(context);
        _context.takenProduct= _context.targetProductTable.TakeProduct();
        _context.currentPath=_context.navigation.GetPathFromTableToAssembler(_context.targetProductTable);
        ChangeState(WorkerGoToAssemblerState.StateType);
    }

    public override void InterruptState()
    {
     
    }
}