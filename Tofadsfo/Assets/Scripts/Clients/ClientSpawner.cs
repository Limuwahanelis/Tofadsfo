using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    public Action<RecipeSO> OnAllCientsFromOrder;
    public Action<ClientController> OnClientSpawned;
    public Action OnClientSkipped;
    [SerializeField] GameObject _clientPrefab;
    [SerializeField] Transform _clientSpawnTran;
    [SerializeField] List<Register> _registers;
    [SerializeField] Transform _door;
    [SerializeField] LevelTimeDisplay _levelTimeDisplay;
    [SerializeField] bool _startSpawn = false;
    float _timetoSpawnClient;
    private List<RecipeSO> _orders;
    private List<int> _spawnedClientsPerOrder=new List<int>();
    private List<int> _amountOfOrders=new List<int>();
    private List<List<int>> _clientsPerRegisterNum=new List<List<int>>();
    private List<List<Register>> _sortedRegisters=new List<List<Register>>();
    private float _timer = 0;
    private float _remainingTime = 0;
    private int _productIndex = 0;
    private int _registerIndex = 0;
    private float _levelTime;
    private bool countTime = false;
    private void Start()
    {
        
    }
    private void Update()
    {
        if (countTime)
        {
            _remainingTime -= (Time.deltaTime * PauseSettings.TimeSpeed);
            _remainingTime = math.clamp(_remainingTime, 0, _levelTime);
            if(_remainingTime<0)
            {
                countTime = false;
            }
            _levelTimeDisplay.SetRemainingTime(_remainingTime);
        }
        if (!_startSpawn) return;
        _timer += Time.deltaTime * PauseSettings.TimeSpeed;

        if (_timer>=_timetoSpawnClient)
        {
            if(_spawnedClientsPerOrder[_productIndex] == _amountOfOrders[_productIndex])
            {
                OnAllCientsFromOrder?.Invoke(_orders[_productIndex]);
                int orderIndexLeftWithClients = -1;
                // Search for orders which have clients left.
                for (int i = 0; i < _spawnedClientsPerOrder.Count; i++)
                {

                    if (_spawnedClientsPerOrder[i] < _amountOfOrders[i])
                    {
                        if(i>_productIndex) // if new order have higher index jump to it.
                        {
                            _productIndex = i;
                            _registerIndex = 0;
                            break;
                        }
                        else // if order have index less than current order index take not of it.
                        {
                            orderIndexLeftWithClients = i;
                        }
                    }
                    if (orderIndexLeftWithClients == -1) // No order left with clients.
                    {
                        _startSpawn = false;
                        //_levelTimeDisplay.SetRemainingTime(0);
                        return;
                    }
                    else
                    {
                        _productIndex = orderIndexLeftWithClients;
                        _registerIndex = 0;
                    }
                }
            }
            if(!_sortedRegisters[_productIndex][_registerIndex].HasFreePlaceInQueue())
            {
                _timer = 0;
                _registerIndex++;
                if (_registerIndex >= _sortedRegisters[_productIndex].Count)
                {
                    _spawnedClientsPerOrder[_productIndex]++;
                    OnClientSkipped?.Invoke();
                    _registerIndex = 0;
                    _productIndex++;
                    if (_productIndex >= _orders.Count)
                    {
                        _productIndex = 0;
                    }
                }
                return;
            }
            SpawnClient(2, _sortedRegisters[_productIndex][_registerIndex]);
            _clientsPerRegisterNum[_productIndex][_registerIndex]++;
            _registerIndex++;
            _spawnedClientsPerOrder[_productIndex]++;
            if (_registerIndex >= _sortedRegisters[_productIndex].Count)
            {
                _productIndex++;
                _registerIndex = 0;
                if (_productIndex >= _orders.Count)
                {
                    _productIndex = 0;
                }
            }
            _timer = 0;
        }
    }
    public void SetUp(LevelInfoSO levelInfo)
    {
        _orders = levelInfo.Orders;
        _amountOfOrders = levelInfo.AmountOfOrders;
        for (int i = 0; i < levelInfo.Orders.Count; i++)
        {
            _spawnedClientsPerOrder.Add(0);
            _clientsPerRegisterNum.Add(new List<int>());
            _sortedRegisters.Add(new List<Register>());
        }
        int totalOrders=0;
        for(int i=0;i<_amountOfOrders.Count;i++) 
        {
            totalOrders+= _amountOfOrders[i];
        }
        _levelTime = levelInfo.LevelTimeInSecnods;
        _timetoSpawnClient = _levelTime / totalOrders;


    }
    // used by a button
    public void SetRegisters()
    {
        for (int i = 0; i < _orders.Count; i++)
        {
            List<Register> list = _registers.Where(x => x.Product == _orders[i].Result).ToList();
            _sortedRegisters[i]=list;
        }
        for (int i = 0; i < _clientsPerRegisterNum.Count; i++)
        {
            for (int j = 0; j < _sortedRegisters[i].Count; j++)
            {
                _clientsPerRegisterNum[i].Add(0);
            }
        }
    }
    // used by a button
    public void SetSpawn(bool value)
    {
        _remainingTime = _levelTime;
            countTime = value;
        _startSpawn = value;
    }
    public void SpawnClient(int num,Register register)
    {
        ClientController client= Instantiate(_clientPrefab, _clientSpawnTran.position,_clientPrefab.transform.rotation).GetComponent<ClientController>();
        List<Vector2> pahtTODoor = new List<Vector2>() {new Vector2() };
        client.SetUp(num, register, _door);
        OnClientSpawned?.Invoke(client);
    }
}
