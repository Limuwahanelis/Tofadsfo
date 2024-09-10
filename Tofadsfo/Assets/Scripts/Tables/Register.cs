using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Register : MonoBehaviour
{
    public ProductSO Product => _product;
    [SerializeField] ProductSO _product;
    public Action OnOrderDelivered;
    public bool IsOrderRequested => _isOrderRequested;
    private bool _isOrderRequested=false;
    private bool _isOrderOnRegister;
    public void PlaceOrder()
    {
        if (_isOrderOnRegister)
        {
            OnOrderDelivered?.Invoke();
        }
        else
        {
            _isOrderRequested = true;
        }
    }
    public void TakeOrder()
    {
        _isOrderOnRegister = false;
        _isOrderRequested = false;
    }
    public void DeliverOrder()
    {
        OnOrderDelivered?.Invoke();
    }
}
