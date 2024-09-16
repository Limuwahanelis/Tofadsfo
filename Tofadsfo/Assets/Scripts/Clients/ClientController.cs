using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ClientController : MonoBehaviour
{
    public Action OnClientServed;
    [SerializeField] ProductSO _requestedProduct;
    [SerializeField] ClientQueue _queue;
    [SerializeField] float _timeToDeliverFood;
    [SerializeField] TMP_Text test_text;
    [SerializeField] Register _register;
    protected Dictionary<Type, ClientState> _clientStates = new Dictionary<Type, ClientState>();
    protected ClientState _currenStatet;
    protected ClientContext _context;
    private Transform _door;
    //Vector2Int 
    //bool _isMovingToRegister;

    private void Start()
    {
        SetUp();
    }
    private void Update()
    {
        _currenStatet.Update();
    }
    public void SetUp(int number,Register register,Transform door)
    {
        _queue = register.RegisterQueue;
        _door = door;
        test_text.text = number.ToString();
        _register = register;
    }
    private void SetUp()
    {
        List<Type> states = AppDomain.CurrentDomain.GetAssemblies().SelectMany(domainAssembly => domainAssembly.GetTypes())
.Where(type => typeof(ClientState).IsAssignableFrom(type) && !type.IsAbstract).ToArray().ToList();

        ClientContext _context = new ClientContext()
        {
            ChangeState=ChangeState,
            queue= _queue,
            transform=transform,
            register= _register,
            door=_door,
            ServeClient=OnClientServed,
        };

        ClientState.GetState getState = GetState;
        foreach (Type state in states)
        {
            _clientStates.Add(state, (ClientState)Activator.CreateInstance(state, getState));
        }
        ClientState newState = GetState(ClientInitialState.StateType);
        _currenStatet = newState;
        newState.SetUpState(_context);
    }
    public ClientState GetState(Type state)
    {
        return _clientStates[state];
    }
    public void ChangeState(ClientState newState)
    {
        //if (_printState) Logger.Log(newState.GetType());
        _currenStatet.InterruptState();
        _currenStatet = newState;
    }

}
