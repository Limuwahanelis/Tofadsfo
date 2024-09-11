using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New level info",menuName ="Level info")]
public class LevelInfoSO : ScriptableObject
{
    public int StartingMoney => _startingMoney;
    public List<RecipeSO> Orders => _orders;
    public List<int> AmountOfOrders=>_amountOfOrders;
    public List<ProductSO> AvailableProducts => _availableProducts;
    public List<int> StartingAmountOfIngredients => _startingAmountOfIngredients;
    public List<int> MaximumAmountOfIngredients => _maximumAmountOfIngredients;

    [SerializeField] int _startingMoney;
    [SerializeField] List<RecipeSO> _orders;
    [SerializeField] List<int> _amountOfOrders;
    [SerializeField] List<ProductSO> _availableProducts;
    [SerializeField] List<int> _startingAmountOfIngredients;
    [SerializeField] List<int> _maximumAmountOfIngredients;

}
