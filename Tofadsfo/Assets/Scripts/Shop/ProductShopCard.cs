using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductShopCard : MonoBehaviour
{
    /// <summary>
    /// first int - price, second int - amount
    /// </summary>
    public Action<ProductSO,int, int> OnProductBought;
    [SerializeField] ProductSO _product;
    [SerializeField] TMP_Text _productNameText;
    [SerializeField] Image _productIcon;
    [SerializeField] TMP_Text _priceText;

    private int _amountToBuy;
    
    public void SetAmountToBuy(string amount)
    {
        _amountToBuy = int.Parse(amount);
    }

    public void Buy()
    {
        OnProductBought?.Invoke(_product,_product.ShopPrice, _amountToBuy);
    }

    private void OnValidate()
    {
        if(_product != null) 
        {
            _productNameText.text = _product.name;
            _productIcon.sprite= _product.Icon;
            _priceText.text = $"<color=red>{_product.ShopPrice}$";
        }
    }

}
