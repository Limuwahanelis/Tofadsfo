using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerAtAssemblerState : WorkerState
{
    public static Type StateType { get => typeof(WorkerAtAssemblerState); }
    public WorkerAtAssemblerState(GetState function) : base(function)
    {
    }

    public override void Update()
    {

    }

    public override void SetUpState(WorkerContext context)
    {
        base.SetUpState(context);
        _context.associatedAssembler.AddIngredient(_context.takenProduct);
        ProductSO productToGet = _context.associatedAssembler.GetMissingProduct();
        if (productToGet == null)
        {
            _context.takenProduct= _context.associatedAssembler.Assemble();
            Logger.Log("Assemble");
        }
        else
        {
            _context.currentPath= _context.navigation.GetShortestPathFromAssemblerToProduct(productToGet, out TableWithProducts table);
            _context.targetProductTable = table;
            ChangeState(WorkerGoToProductState.StateType);
        }

    }

    public override void InterruptState()
    {
     
    }
}