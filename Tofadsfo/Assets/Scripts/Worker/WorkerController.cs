using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WorkerController : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] WorkerNavigation _nav;
    [SerializeField] RecipeAssembly _associatedAssembler;
    [SerializeField] TableProductsManagment _tableProductsManagment;
    private List<Vector2Int> _currentPath;
    private bool _go = false;

    protected Dictionary<Type, WorkerState> _workerStates = new Dictionary<Type, WorkerState>();
    protected WorkerState _currenStatet;
    protected WorkerContext _context;
    private void Start()
    {
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
    public void SetPath(List<Vector2Int> path)
    {
        _currentPath = path;
        
        _go = true;
    }
}
