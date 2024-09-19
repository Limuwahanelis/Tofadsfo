using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Register : MonoBehaviour
{
    public Transform AccessPoint => _accessPoint;
    public Action<int> OnItemBought;
    public ProductSO Product => _recipe.Result;
    [SerializeField] RecipeSO _recipe;
    public Action OnOrderDelivered;
    public ClientQueue RegisterQueue => _queue;

    [SerializeField] SpriteRenderer _productIcon;
    [SerializeField] ClientQueue _queue;
    [SerializeField] Transform _accessPoint;
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
        OnItemBought?.Invoke(_recipe.Price);
    }
    public void DeliverOrder()
    {
        OnOrderDelivered?.Invoke();
    }

    private void OnValidate()
    {
        if(_recipe.Result != null) 
        {
            _productIcon.sprite = _recipe.Result.Icon;
        }
    }
}
