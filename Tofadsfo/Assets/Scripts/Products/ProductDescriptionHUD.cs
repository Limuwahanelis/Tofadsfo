using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductDescriptionHUD : MonoBehaviour
{
    [SerializeField] Image _productImage;
    [SerializeField] ProductSO _product;
    [SerializeField] TMP_Text _text;
    [SerializeField] ProductsStats _stats;
    private void OnValidate()
    {
        if(_product != null )
        {
            _productImage.sprite = _product.Icon;
        }
    }

    private void Start()
    {
        var (max, curr) = _stats.GetProductAmounts(_product);
        _text.text = $"{curr}/{max}";
    }
}
