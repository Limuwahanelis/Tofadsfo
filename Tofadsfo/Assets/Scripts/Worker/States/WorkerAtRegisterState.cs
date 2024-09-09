using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerAtRegisterState : WorkerState
{
    public static Type StateType { get => typeof(WorkerAtRegisterState); }
    public WorkerAtRegisterState(GetState function) : base(function)
    {
    }

    public override void Update()
    {

    }

    public override void SetUpState(WorkerContext context)
    {
        base.SetUpState(context);
        Logger.Log("AT register");
      
    }
    public override void Test()
    {
        List<Vector2Int> path = _workerContext.navigation.GetShortestPathFromRegisterToProduct(_workerContext.associatedAssembler.Recipe.Ingredients[0]);
        for (int i=0;i<path.Count;i++)
        {
            Logger.Log(path[i]);
        }
        _workerContext.currentPath = path;
        ChangeState(WorkerGoToProductState.StateType);
    }
    public override void InterruptState()
    {
     
    }
}