using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField, Header("DEBUG")] bool _debug;
#endif
    [Space]
    [SerializeField] LevelInfoSO _levelInfo;
    [SerializeField] LevelInfoDisplay _levelInfoDisplay;
    [SerializeField] PlayerMouseInteractions _mouseInteractions;
    [SerializeField] TableProductsManagment _tableProductsManagment;
    [SerializeField] ClientSpawner _clientSpawner;
    [SerializeField] LevelTimeDisplay _levelTimeDisplay;
    [SerializeField] ProductsShop _shop;
    [SerializeField] MoneyInfo _moneyInfo;
    
    [SerializeField] LevelRecipesInfoDisplay _levelRecipesInfoDisplay;
    private void Awake()
    {
        _moneyInfo.SetUp(_levelInfo.StartingMoney);
    }
    // Start is called before the first frame update
    void Start()
    {
        
        _mouseInteractions.SetInteraction(false);
        _levelInfoDisplay.Show();
#if UNITY_EDITOR
        if (_debug)
        {
            _mouseInteractions.SetInteraction(true);
            _levelInfoDisplay.Hide();
        }
#endif
       
        _levelRecipesInfoDisplay.SetUp(_levelInfo);
        _levelInfoDisplay.SetOrders(_levelInfo);
        _shop.SetUpShop(_levelInfo);
        _levelTimeDisplay.SetTime(_levelInfo.LevelTimeInSecnods);
        _tableProductsManagment.SetUp(_levelInfo.AvailableProducts);
        _clientSpawner.SetUp(_levelInfo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
