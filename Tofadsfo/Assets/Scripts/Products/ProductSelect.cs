using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductSelect : MonoBehaviour
{
    [SerializeField]List<ProductDescription> _products = new List<ProductDescription>();
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
}
