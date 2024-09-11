using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Search;

public class LevelInfoDisplay : MonoBehaviour
{
    
    [SerializeField,SearchContext("t:OrderCard")] GameObject _orderCardPrefab;
    [SerializeField] GameObject _ordersLayout;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetOrders(LevelInfoSO levelInfo)
    {
        for(int i=0;i<levelInfo.Orders.Count;i++) 
        {
            OrderCard ordercard= Instantiate(_orderCardPrefab, _ordersLayout.transform).GetComponent<OrderCard>();
            ordercard.SetRecipe(levelInfo.Orders[i], levelInfo.AmountOfOrders[i]);
        }
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
