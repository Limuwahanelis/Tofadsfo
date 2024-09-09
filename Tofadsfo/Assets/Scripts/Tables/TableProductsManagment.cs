using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class TableProductsManagment : MonoBehaviour
{
    [SerializeField] ProductsStats _productsStats;
    [SerializeField] List<ProductSO> _products = new List<ProductSO>();
    List<int> _tablesWithProduct=new List<int>();
    List<List<TableWithProducts>> _tables = new List<List<TableWithProducts>>();
    private void Start()
    {
        for(int i=0;i<_products.Count;i++)
        {
            _tablesWithProduct.Add(0);
            _tables.Add(new List<TableWithProducts>());
        }
    }
    public List<TableWithProducts> GetAllTablesWithProduct(ProductSO product)
    {
        int index = _products.IndexOf(product);
        return _tables[index];
    }
    public void RemoveProductFromTable(ProductSO product, TableWithProducts table)
    {
        int index = _products.IndexOf(product);
        if (!_tables[index].Contains(table)) return;
        _tables[index].Remove(table);
        for(int i=0;i<_products.Count;i++)
        {
            UpdateTables(i);
        }
        
    }
    public void AddProductToATable(ProductSO product, TableWithProducts table)
    {

        int index = _products.IndexOf(product);
        int indexProductToRemove=_products.IndexOf(table.AssociatedProdct);
        if (indexProductToRemove != -1)
        {
            if (_tables[indexProductToRemove].Contains(table))
            {
                _tables[indexProductToRemove].Remove(table);
                UpdateTables(indexProductToRemove);
            }
        }
        if (_tables[index].Contains(table))
        {
            Logger.Warning("Trying to add the same table twice");
            return;
        }
        _tables[index].Add(table);
        
        UpdateTables( index);
    }

    private void UpdateTables(int index)
    {
        int total = _productsStats.GetProductAmounts(_products[index]).Item1;
        int remainder = 0;
        int amountPertable = total;
        if (_tables[index].Count != 0)
        {
            remainder = total % _tables[index].Count;
            amountPertable = total / _tables[index].Count;
        }
        //int tmp =((int)math.ceil( total / (float)_tables[index].Count));
        for (int j = 0; j < _tables[index].Count; j++)
        {

            int amount = amountPertable;
            if (j == 0)
            {
                if (remainder != 0)
                {
                    amount++;
                }
            }
            if (amount > total)
            {
                amount = total;
            }
            // Check if table can contain assigned amount.
            if (amount > _tables[index][j].MaxProductAmount)
            {
                amount = _tables[index][j].MaxProductAmount;
            }

            total -= amount;
            _tables[index][j].SetProductAmount(amount);
        }
    }


}
