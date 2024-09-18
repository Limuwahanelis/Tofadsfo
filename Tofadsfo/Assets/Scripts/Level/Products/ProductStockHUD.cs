using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductStockHUD : MonoBehaviour
{
    [SerializeField] Image _productImage;
    [SerializeField] ProductSO _product;
    [SerializeField] TMP_Text _text;
    [SerializeField] ProductsStats _stats;
    private int _productMaxAmount;
    private void OnValidate()
    {
        if(_product != null )
        {
            _productImage.sprite = _product.Icon;
        }
    }

    private void Start()
    {
        _stats.OnCurrentProductAmountChanged += UpdateProduct;
        var (max, curr) = _stats.GetProductAmounts(_product);
        _productMaxAmount = max;
        _text.text = $"{curr}/{max}";
    }
    private void UpdateProduct(ProductSO product,int amount)
    {
        if (product != _product) return;
        _text.text= $"{amount}/{_productMaxAmount}";
    }

    private void OnDestroy()
    {
        _stats.OnCurrentProductAmountChanged -= UpdateProduct;
    }
}
