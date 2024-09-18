using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyInfo : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] bool _debug;
#endif
    public Action<int> OnMoneyCahnged;
    public int CurrentMoney=>_currentMoney;
    private int _currentMoney;
    public void SetUp(int levelStartingMoney)
    {
        _currentMoney = levelStartingMoney;
#if UNITY_EDITOR 
        if (!_debug) _currentMoney += GameSaver.GameData.savedMoney;
#else
        _currentMoney += GameSaver.GameData.savedMoney;
#endif
        SetMoney(_currentMoney);
    }
    public void ReduceMoney(int value)
    {
        _currentMoney -= value;
        OnMoneyCahnged?.Invoke(_currentMoney);
    }
    public void AddMoney(int value)
    {
        _currentMoney += value;
        OnMoneyCahnged?.Invoke(_currentMoney);
    }
    public void SetMoney(int value)
    {
        _currentMoney = value;
        OnMoneyCahnged?.Invoke(_currentMoney);
    }
}
