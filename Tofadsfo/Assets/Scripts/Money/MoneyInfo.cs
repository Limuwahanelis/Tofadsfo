using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyInfo : MonoBehaviour
{
    public Action<int> OnMoneyCahnged;
    public int CurrentMoney=>_currentMoney;
    [SerializeField]int _currentMoney;
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
