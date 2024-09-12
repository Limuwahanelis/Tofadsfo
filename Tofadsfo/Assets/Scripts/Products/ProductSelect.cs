using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProductSelect : MonoBehaviour
{
    [SerializeField] GameObject _prodcuButtonPrefab;
    [SerializeField] GameObject _buttonsHolder;
    [SerializeField]List<ProductDescription> _products = new List<ProductDescription>();
    [SerializeField] ProductSO _emptyProduct;
    private TableWithProducts _table;
    public void ShowList(TableWithProducts table)
    {
        _table = table;
        gameObject.SetActive(true);
    }
    public void SetProductOnTable(int productIndex)
    {
        _table.SetProduct(_products[productIndex].Product);
    }
    public void SetUp(List<ProductSO> _availableProducts)
    {

        for(int i=0;i<_availableProducts.Count;i++) 
        {
            ProductDescription productDescription= Instantiate(_prodcuButtonPrefab, _buttonsHolder.transform).GetComponent<ProductDescription>();
            productDescription.SetUp(_availableProducts[i],i+1);
            _products.Add(productDescription);
            productDescription.OnProductSelected += SetProductOnTable;
        }
        RectTransform tra = _buttonsHolder.GetComponent<RectTransform>();
        tra.sizeDelta = new Vector2(tra.sizeDelta.x, (_products.Count+1) * 50);
        _buttonsHolder.GetComponent<RectTransform>().sizeDelta = tra.sizeDelta;
        //Rect rect = gameObject.GetComponent<RectTransform>().rect;
        //rect.height=
    }
    private void OnDestroy()
    {
        for (int i = 0; i < _products.Count; i++)
        {

            _products[i].OnProductSelected -= SetProductOnTable;
        }
    }
}
