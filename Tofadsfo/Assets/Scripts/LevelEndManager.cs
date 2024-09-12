using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndManager : MonoBehaviour
{
    public Action OnLevelEnd;
    [SerializeField] List<WorkerController> _workers = new List<WorkerController>();
    [SerializeField] List<Register> _register= new List<Register>();
    [SerializeField] LevelEndDisplay _levelEndDisplay;
    [SerializeField] MoneyInfo _moneyInfo;
    [SerializeField] LevelInfoSO _levelInfoSO;
    private int _notWorkingWorkerCount = 0;
    private int _earnedMoney = 0;
    private int _balance = 0;
    private void Start()
    {
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
        OnLevelEnd?.Invoke();
    }
    private void IncreaseNotWorkingWorkers()
    {
        _notWorkingWorkerCount++;
        if(_notWorkingWorkerCount==_workers.Count)
        {
            EndLevel();
        }
    }
    private void IncreaseMoney(int money)
    {
        _earnedMoney += money;
        _moneyInfo.AddMoney(money);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _workers.Count; i++)
        {
            _workers[i].OnWorkerNoIngredients -= IncreaseNotWorkingWorkers;
        }
        for (int i = 0; i < _register.Count; i++)
        {
            _register[i].OnItemBought -= IncreaseMoney;
        }
    }
}
