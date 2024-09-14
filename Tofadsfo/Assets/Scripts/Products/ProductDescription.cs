using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductDescription : MonoBehaviour
{
    public Action<int> OnProductSelected;
    public ProductSO Product => _productSO;
    [SerializeField] Image _image;
    [SerializeField] ProductSO _productSO;
    private int _index;

    public void SetUp(ProductSO product,int index)
    {
        _image.sprite = product.Icon;
        _productSO = product;
        _index = index;
    }
    public void SelectProduct()
    {
        OnProductSelected?.Invoke(_index);
    }
    //private void OnValidate()
    //{
    //    if (_productSO != null)
    //    {
    //        _image.sprite = _productSO.Icon;
    //    }
    //}

}
