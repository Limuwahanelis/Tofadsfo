using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TableWithProducts : MonoBehaviour,IInteractable
{
    [SerializeField] GameObject _productListUI;
    [SerializeField] SpriteRenderer _productSprite;
    public void Interact()
    {
        ShowProductSelection();
    }
    public void ShowProductSelection()
    {
        _productListUI.SetActive(true);
    }

}
