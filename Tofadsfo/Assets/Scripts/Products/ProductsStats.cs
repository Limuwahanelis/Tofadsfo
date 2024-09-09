using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductsStats : MonoBehaviour
{
    public Action<ProductSO, int> OnCurrentProductAmountChanged;
    [SerializeField] List<ProductSO> _products=new List<ProductSO>();
    [SerializeField] List<int> _maxAmounts;
    [SerializeField] List<int> _currentAmounts;

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
    public void OncreaseProductAmount(ProductSO product, int amount)
    {
        int index = _products.IndexOf(product);
        _currentAmounts[index] += amount;
        OnCurrentProductAmountChanged?.Invoke(product, _currentAmounts[index]);
    }

}
