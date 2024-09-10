using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerGotoRegisterState : WorkerState
{
    public static Type StateType { get => typeof(WorkerGotoRegisterState); }
    public WorkerGotoRegisterState(GetState function) : base(function)
    {
    }

    public override void Update()
    {

    }

    public override void SetUpState(WorkerContext context)
    {
        base.SetUpState(context);
    }

    public override void InterruptState()
    {
     
    }
}