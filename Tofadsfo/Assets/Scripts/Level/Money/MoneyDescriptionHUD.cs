using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyDescriptionHUD : MonoBehaviour
{
    [SerializeField] MoneyInfo _moneyInfo;
    [SerializeField] TMP_Text _text;
    // Start is called before the first frame update
    void Start()
    {
        _moneyInfo.OnMoneyCahnged += SetMoney;
        SetMoney(_moneyInfo.CurrentMoney);
    }
    private void SetMoney(int value)
    {
        _text.text = $"$ {value}";
    }
    private void OnDestroy()
    {
        _moneyInfo.OnMoneyCahnged -= SetMoney;
    }
}
