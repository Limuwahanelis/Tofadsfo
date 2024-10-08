using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TableWithProducts : MonoBehaviour,IInteractable
{
    public ProductSO AssociatedProdct => _associatedProduct;
    public int MaxProductAmount => _maxProductAmount;
    public int CurrentProductAmount => _currentProductAmount;
    public List<Transform> AccessPoints => _accessPoints;
    [SerializeField] List<Transform>_accessPoints = new List<Transform>();
    [SerializeField] ProductSelect _productListUI;
    [SerializeField] SpriteRenderer _productSprite;
    [SerializeField] int _maxProductAmount;
    [SerializeField] TMP_Text _productAmountDisplay;
    [SerializeField] TMP_Text _maxProductAmountDisplay;
    [SerializeField] ProductsStats _productStats;
    [SerializeField] TableProductsManagment _tableProductsManagment;
    [SerializeField] ProductSO _associatedProduct;
    private int _currentProductAmount;
    private void Awake()
    {
        _maxProductAmountDisplay.text = _maxProductAmount.ToString();
    }
    public void Interact()
    {
        ShowProductSelection();
    }
    public void ShowProductSelection()
    {
        _productListUI.ShowList(this);
    }
    public ProductSO TakeProduct()
    {
        _currentProductAmount -= 1;
        _productAmountDisplay.text = _currentProductAmount.ToString();
        return _associatedProduct;
    }
    public virtual void SetProduct(ProductSO product)
    {
        _productSprite.sprite = product.Icon;
        //_tableProductsManagment.RemoveProductFromTable(product, this);
        _tableProductsManagment.AddProductToATable(product, this);
        _associatedProduct = product;
        _tableProductsManagment.UpdateWorkersnavigations(this);
    }
    public void SetProductAmount(int amount)
    {
        _currentProductAmount = amount;
        _productAmountDisplay.text = _currentProductAmount.ToString();
    }
    private void OnValidate()
    {
        _maxProductAmountDisplay.text=_maxProductAmount.ToString();
    }

}
