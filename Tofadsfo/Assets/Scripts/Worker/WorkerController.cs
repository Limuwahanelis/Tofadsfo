using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WorkerController : MonoBehaviour
{
    public Action OnWorkerNoIngredients;
    [SerializeField] float _speed;
    [SerializeField] WorkerNavigation _nav;
    [SerializeField] RecipeAssembly _associatedAssembler;
    [SerializeField] TableProductsManagment _tableProductsManagment;
    [SerializeField] Register _register;

    protected Dictionary<Type, WorkerState> _workerStates = new Dictionary<Type, WorkerState>();
    protected WorkerState _currenStatet;
    protected WorkerContext _context;
    private void Start()
    {
        _nav.AssignRegisterAndAssembler(_register, _associatedAssembler);
        SetUpWorker();

    }
    private void Update()
    {
        _currenStatet.Update();

    }
    public void Test()
    {
        _currenStatet.Test();
    }
    private void SetUpWorker()
    {
        List<Type> states = AppDomain.CurrentDomain.GetAssemblies().SelectMany(domainAssembly => domainAssembly.GetTypes())
.Where(type => typeof(WorkerState).IsAssignableFrom(type) && !type.IsAbstract).ToArray().ToList();

        WorkerContext _context = new WorkerContext()
        {
            ChangeState = ChangeState,
            navigation = _nav,
            associatedAssembler=_associatedAssembler,
            workerTran=transform,
            tableProductsManagment=_tableProductsManagment,
            register=_register,
            WorkDone=OnWorkerNoIngredients,
        };

        WorkerState.GetState getState = GetState;
        foreach (Type state in states)
        {
            _workerStates.Add(state, (WorkerState)Activator.CreateInstance(state, getState));
        }
        WorkerState newState = GetState(WorkerAtRegisterState.StateType);
        newState.SetUpState(_context);
        _currenStatet = newState;
    }
    public WorkerState GetState(Type state)
    {
        return _workerStates[state];
    }
    public void ChangeState(WorkerState newState)
    {
        //if (_printState) Logger.Log(newState.GetType());
        _currenStatet.InterruptState();
        _currenStatet = newState;
    }
}
