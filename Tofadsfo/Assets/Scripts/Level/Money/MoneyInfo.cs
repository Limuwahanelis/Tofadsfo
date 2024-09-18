using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class MoneyInfo : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] bool _debug;
#endif
    public Action<int> OnMoneyCahnged;
    public int CurrentMoney=>_currentMoney;
    [SerializeField] LevelInfoSO _levelInfo;
    private int _currentMoney;
    private void Awake()
    {
        _currentMoney= _levelInfo.StartingMoney;
#if UNITY_EDITOR 
        if(!_debug) _currentMoney += GameSaver.GameData.savedMoney;
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
