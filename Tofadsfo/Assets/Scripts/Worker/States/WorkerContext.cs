using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerContext
{
    public Action<WorkerState> ChangeState;
    public WorkerNavigation navigation;
    public RecipeAssembly associatedAssembler;
    public List<Vector2Int> currentPath;
    public Transform workerTran;
}
