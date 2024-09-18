using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Search;

public class LevelInfoDisplay : MonoBehaviour
{
    
    [SerializeField,SearchContext("t:OrderCard")] GameObject _orderCardPrefab;
    [SerializeField] GameObject _ordersLayout;
    [SerializeField] TMP_Text _requiredMoneyText;
    [SerializeField] GameObject _canvas;
    int _moneyRequiredForLevel =0;
    public void SetOrders(LevelInfoSO levelInfo)
    {
        for(int i=0;i<levelInfo.Orders.Count;i++) 
        {
            OrderCard ordercard= Instantiate(_orderCardPrefab, _ordersLayout.transform).GetComponent<OrderCard>();
            ordercard.SetRecipe(levelInfo.Orders[i], levelInfo.AmountOfOrders[i]);
            _moneyRequiredForLevel += levelInfo.Orders[i].Price * levelInfo.AmountOfOrders[i];
        }
        _moneyRequiredForLevel = ((int)math.ceil( _moneyRequiredForLevel * 0.9f));
        _requiredMoneyText.text = _moneyRequiredForLevel.ToString()+"$";
    }
    public void Show()
    {
        _canvas.SetActive(true);
    }
    public void Hide()
    {
        _canvas.SetActive(false);
    }
}
