using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName ="New level info",menuName ="Level info")]
// has cutom inpector
public class LevelInfoSO : ScriptableObject
{
    public int StartingMoney => _startingMoney;
    public float LevelTimeInSecnods => _levelTimeInSeconds;
    public List<RecipeSO> Orders => _orders;
    public List<int> AmountOfOrders=>_amountOfOrders;
    public List<ProductSO> AvailableProducts => _availableProducts;
    public List<int> StartingAmountOfIngredients => _startingAmountOfIngredients;
    public List<int> MaximumAmountOfIngredients => _maximumAmountOfIngredients;

    [SerializeField] int _startingMoney;
    [SerializeField] int _levelTimeInSeconds;
    [SerializeField] List<RecipeSO> _orders;
    [SerializeField] List<int> _amountOfOrders;
    [SerializeField] List<ProductSO> _availableProducts;
    [SerializeField] List<int> _startingAmountOfIngredients;
    [SerializeField] List<int> _maximumAmountOfIngredients;

    public int GetMoneyRequiredForALevel()
    {
        int moneyForLevel = 0;
        for (int i = 0; i < Orders.Count; i++)
        {
            moneyForLevel += Orders[i].Price * AmountOfOrders[i];
        }
        moneyForLevel = ((int)math.ceil(moneyForLevel * 0.9f));
        return moneyForLevel;
    }

}
