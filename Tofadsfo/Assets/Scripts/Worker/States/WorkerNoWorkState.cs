using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerNoWorkState : WorkerState
{
    public static Type StateType { get => typeof(WorkerNoWorkState); }
    public WorkerNoWorkState(GetState function) : base(function)
    {
    }

    public override void Update()
    {

    }

    public override void SetUpState(WorkerContext context)
    {
        base.SetUpState(context);
        Logger.Log("NO work");
    }

    public override void InterruptState()
    {
     
    }
}