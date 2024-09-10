using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerAtRegisterState : WorkerState
{
    private bool gd = false;
    public static Type StateType { get => typeof(WorkerAtRegisterState); }
    public WorkerAtRegisterState(GetState function) : base(function)
    {
    }

    public override void Update()
    {
        if(_context.register.IsOrderRequested)
        {
            if (_context.tableProductsManagment.IsThereEnoughProductsForARecipe(_context.associatedAssembler.Shortrecipe))
            {
                Logger.Log("eng");
                List<Vector2Int> path = _context.navigation.GetShortestPathFromRegisterToProduct(_context.associatedAssembler.Recipe.Ingredients[0], out TableWithProducts table);
                _context.currentPath = path;
                _context.targetProductTable = table;
                ChangeState(WorkerGoToProductState.StateType);
            }
            else
            {
                Logger.Log("NOt en");
            }
            // choose path to product
            Logger.Log("AT register");
        }
    }

    public override void SetUpState(WorkerContext context)
    {
        base.SetUpState(context);
        if(_context.takenProduct==_context.register.Product)
        {
            _context.register.DeliverOrder();
            _context.takenProduct = null;
        }
    }
    public override void Test()
    {
        gd = true;

    }
    public override void InterruptState()
    {
     
    }
}