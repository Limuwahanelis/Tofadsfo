using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ProductsStats : MonoBehaviour
{
    public Action<ProductSO, int> OnCurrentProductAmountChanged;
    [SerializeField] LevelInfoSO _levelInfo;
    [SerializeField] ProductSelect _productSelect;
    private List<ProductSO> _products=new List<ProductSO>();
    private List<int> _maxAmounts;
    private List<int> _currentAmounts;
    private void Awake()
    {
        _products = _levelInfo.AvailableProducts;
        _productSelect.SetUp(_products);
        _maxAmounts = _levelInfo.MaximumAmountOfIngredients;
        _currentAmounts = _levelInfo.StartingAmountOfIngredients;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="product"></param>
    /// <returns>(Product max amount, current product amount)</returns>
    public (int,int) GetProductAmounts(ProductSO product)
    {
        int index=_products.IndexOf(product);
        return (_maxAmounts[index], _currentAmounts[index]);
    }
    public void ReduceProductAmount(ProductSO product,int amount)
    {
        int index = _products.IndexOf(product);
        _currentAmounts[index] -= amount;
        OnCurrentProductAmountChanged?.Invoke(product, _currentAmounts[index]);
    }
    public void OnIcreaseProductAmount(ProductSO product, int amount)
    {
        int index = _products.IndexOf(product);
        _currentAmounts[index] += amount;
        _currentAmounts[index] = math.clamp(_currentAmounts[index], 0, _maxAmounts[index]);
        OnCurrentProductAmountChanged?.Invoke(product, _currentAmounts[index]);
    }

}
