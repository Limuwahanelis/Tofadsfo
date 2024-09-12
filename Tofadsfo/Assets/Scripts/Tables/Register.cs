using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Register : MonoBehaviour
{
    public ProductSO Product => _product;
    [SerializeField] ProductSO _product;
    public Action OnOrderDelivered;
    public ClientQueue RegisterQueue => _queue;

    [SerializeField] SpriteRenderer _productIcon;
    [SerializeField] ClientQueue _queue;
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
    public bool HasFreePlaceInQueue()
    {
        return _queue.HasFreePlace();
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

    private void OnValidate()
    {
        if(_product != null) 
        {
            _productIcon.sprite = _product.Icon;
        }
    }
}
