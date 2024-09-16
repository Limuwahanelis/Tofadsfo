using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndManager : MonoBehaviour
{
    public Action OnLevelEnd;
    [SerializeField] LevelInfoSO _levelInfoSO;
    [SerializeField] LevelEndDisplay _levelEndDisplay;
    [SerializeField] MoneyInfo _moneyInfo;
    [SerializeField] ClientSpawner _clientSpawner;
    [SerializeField] List<WorkerController> _workers = new List<WorkerController>();
    [SerializeField] List<Register> _register= new List<Register>();
    private List<ClientController> _clients = new List<ClientController>();
    private int _notWorkingWorkerCount = 0;
    private int _earnedMoney = 0;
    private int _balance = 0;
    private int _totalClientsNum;
    private int _skippedClinetsNum;
    private int _servedClinetsNum;
    private List<bool> _endedOrders=new List<bool>();
    private void Start()
    {
        _clientSpawner.OnAllCientsFromOrder += EndOrder;
        _clientSpawner.OnClientSkipped += IncreaseSkippedClients;
        _clientSpawner.OnClientSpawned += AddClient;
        for(int i=0;i<_levelInfoSO.Orders.Count;i++)
        {
            _endedOrders.Add(false);
        }
        for(int i=0;i<_levelInfoSO.AmountOfOrders.Count;i++)
        {
            _totalClientsNum += _levelInfoSO.AmountOfOrders[i];
        }
        for(int i=0;i<_workers.Count;i++)
        {
            _workers[i].OnWorkerNoIngredients += IncreaseNotWorkingWorkers;
        }
        for(int i=0;i<_register.Count;i++) 
        {
            _register[i].OnItemBought += IncreaseMoney;
        }
    }
    private void EndLevel()
    {
        Logger.Log($"Level end. earned {_earnedMoney}");
        _balance = _moneyInfo.CurrentMoney - _levelInfoSO.GetMoneyRequiredForALevel();
        _levelEndDisplay.SetUp(_moneyInfo.CurrentMoney, _levelInfoSO.GetMoneyRequiredForALevel(), _balance);
        _levelEndDisplay.gameObject.SetActive(true);
        _levelEndDisplay.transform.parent.gameObject.SetActive(true);
        _clientSpawner.SetSpawn(false);
        OnLevelEnd?.Invoke();
    }
    private void IncreaseSkippedClients()
    {
        _skippedClinetsNum++;
        if(_skippedClinetsNum+_servedClinetsNum==_totalClientsNum)
        {
            EndLevel();
        }
    }
    private void EndOrder(RecipeSO order)
    {
        _endedOrders[_levelInfoSO.Orders.IndexOf(order)] = true;
        if(_endedOrders.Count(x=>x==true)+ _notWorkingWorkerCount==_endedOrders.Count())
        {
            if (_servedClinetsNum + _skippedClinetsNum == _totalClientsNum)
            {
                EndLevel();
            }
        }
    }
    private void AddClient(ClientController client)
    {
        _clients.Add(client);
        client.OnClientServed += IncreaseServedClients;
    }
    private void IncreaseServedClients()
    {
        _servedClinetsNum++;
        if (_skippedClinetsNum + _servedClinetsNum == _totalClientsNum)
        {
            EndLevel();
        }
    }
    private void IncreaseNotWorkingWorkers()
    {
        _notWorkingWorkerCount++;
        if(_notWorkingWorkerCount==_workers.Count)
        {
            EndLevel();
        }
        else if(_endedOrders.Count(x => x == true) + _notWorkingWorkerCount == _endedOrders.Count)
        {
            if (_skippedClinetsNum + _servedClinetsNum == _totalClientsNum)
            {
                EndLevel();
            }
        }
    }
    private void IncreaseMoney(int money)
    {
        _earnedMoney += money;
        _moneyInfo.AddMoney(money);
    }
    public void SaveExtraMoney()
    {
        GameSaver.UpdateMoney(_balance);
        GameSaver.Save();
    }
    public void GoToNextLevel()
    {
        GameSaver.Setlevel(SceneManager.GetActiveScene().buildIndex+1);
        GameSaver.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void OnDestroy()
    {
        for(int i=0;i<_clients.Count;i++)
        {
            _clients[i].OnClientServed -= IncreaseServedClients;
        }
        for (int i = 0; i < _workers.Count; i++)
        {
            _workers[i].OnWorkerNoIngredients -= IncreaseNotWorkingWorkers;
        }
        for (int i = 0; i < _register.Count; i++)
        {
            _register[i].OnItemBought -= IncreaseMoney;
        }
        _clientSpawner.OnAllCientsFromOrder -= EndOrder;
    }
}
