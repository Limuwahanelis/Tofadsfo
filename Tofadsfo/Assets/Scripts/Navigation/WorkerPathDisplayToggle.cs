using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkerPathDisplayToggle : MonoBehaviour
{
    public Action<bool, ProductSO> OnToggled;
    [SerializeField] ProductSO _product;
    [SerializeField] Image _toggleIcon;
    public void SetUp(ProductSO product)
    {
        _product = product;
        _toggleIcon.sprite = _product.Icon;
    }
    public void SetToggle(bool value)
    {
        OnToggled?.Invoke(value, _product);
    }
}
