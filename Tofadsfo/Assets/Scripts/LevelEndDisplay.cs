using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LevelEndDisplay : MonoBehaviour
{
    [SerializeField] float _timeToShowStample = 2f;
    [SerializeField] TMP_Text _moneyText;
    [SerializeField] TMP_Text _rentText;
    [SerializeField] TMP_Text _balanceText;
    [SerializeField] GameObject _completeText;
    [SerializeField] GameObject _failText;
    [SerializeField] GameObject _NextlevelButton;
    [SerializeField] bool _lastlevel = false;
    private bool failed = false;
    public void SetUp(int money,int rent,int balance)
    {
        _moneyText.text=money.ToString()+"$";
        _rentText.text = rent.ToString() + "$";
        if(balance<0)
        {
            _balanceText.color = Color.red;
            _balanceText.text =$"{balance}$";
            failed = true;
        }
        else
        {
            _balanceText.color = Color.green;
            _balanceText.text = $"+{balance}$";
            failed = false;
            
        }
        if(!_lastlevel) _NextlevelButton.SetActive(!failed);
        
    }
    void OnEnable()
    {
        StartCoroutine(StampleCor());
    }
    IEnumerator StampleCor()
    {
        float timer = 0;
        while (timer < _timeToShowStample)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        if (failed) _failText.SetActive(true);
        else _completeText.SetActive(true);
    }
}

