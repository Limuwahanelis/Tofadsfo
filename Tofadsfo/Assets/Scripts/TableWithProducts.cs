using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TableWithProducts : MonoBehaviour,IInteractable
{
    [SerializeField] ProductSelect _productListUI;
    [SerializeField] SpriteRenderer _productSprite;
    private ProductSO _associatedProduct;
    public void Interact()
    {
        ShowProductSelection();
    }
    public void ShowProductSelection()
    {
        _productListUI.ShowList(this);
    }
    public void SetProduct(ProductSO product)
    {
        _associatedProduct = product;
        _productSprite.sprite = product.Icon;
    }

}
