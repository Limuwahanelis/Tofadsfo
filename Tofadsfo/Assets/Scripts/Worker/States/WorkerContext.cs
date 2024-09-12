using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerContext
{
    public Action<WorkerState> ChangeState;
    public Action WorkDone;
    public WorkerNavigation navigation;
    public RecipeAssembly associatedAssembler;
    public List<Vector2Int> currentPath;
    public Transform workerTran;
    public TableWithProducts targetProductTable;
    public ProductSO takenProduct;
    public TableProductsManagment tableProductsManagment;
    public Register register;
    public float speed=6;
    
}
