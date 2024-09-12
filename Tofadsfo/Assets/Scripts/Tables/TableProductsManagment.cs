using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class TableProductsManagment : MonoBehaviour
{
    [SerializeField] ProductSO _emptyProduct;
    [SerializeField] ProductsStats _productsStats;
    [SerializeField] List<WorkerNavigation> _workersNavs;
    List<int> _tablesWithProduct=new List<int>();
    List<List<TableWithProducts>> _tables = new List<List<TableWithProducts>>();
    private List<ProductSO> _products = new List<ProductSO>();
    private List<int> _notReservedProductsAmount;
    public void SetUp(List<ProductSO> products)
    {
        _products = products;
        _notReservedProductsAmount = new List<int>();
        for (int i = 0; i < _products.Count; i++)
        {
            _tablesWithProduct.Add(0);
            _tables.Add(new List<TableWithProducts>());
            _notReservedProductsAmount.Add(0);
        }
    }
    //public List<TableWithProducts> GetAllTablesWithProduct(ProductSO product)
    //{
    //    int index = _products.IndexOf(product);
    //    return _tables[index];
    //}
    public void AddProductToATable(ProductSO product, TableWithProducts table)
    {
        if (product == _emptyProduct && table.AssociatedProdct == _emptyProduct)  return;
        int index = _products.IndexOf(product);
        int indexProductToRemove=_products.IndexOf(table.AssociatedProdct);

        if (indexProductToRemove != -1)
        {
            if (_tables[indexProductToRemove].Contains(table))
            {
                _tables[indexProductToRemove].Remove(table);
                _notReservedProductsAmount[indexProductToRemove] -= table.CurrentProductAmount;
                UpdateTables(indexProductToRemove);
            }
        }
        if (product == _emptyProduct)
        {
            UpdateTables(indexProductToRemove);
            table.SetProductAmount(0);
            return;
        }
        if (_tables[index].Contains(table))
        {
            Logger.Warning("Trying to add the same table twice");
            return;
        }
        _tables[index].Add(table);
        UpdateTables( index);
        _notReservedProductsAmount[index] += table.CurrentProductAmount;
    }
    public void UpdateWorkersnavigations(TableWithProducts table)
    {
        for(int i=0;i<_workersNavs.Count;i++) 
        {
            _workersNavs[i].SetPathsForTable(table);
        }
    }
    private void UpdateTables(int index)
    {
        int total = _productsStats.GetProductAmounts(_products[index]).Item2;
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
    public bool IsThereEnoughProductsForARecipe(RecipeSO.CraftingRecipeShort recipe)
    {
        for (int i = 0; i < recipe.productTypes.Length; i++)
        {
            int index = _products.IndexOf(recipe.productTypes[i]);
            if (_notReservedProductsAmount[index] < recipe.resourcesNum[i]) return false;
        }
        for (int i = 0; i < recipe.productTypes.Length; i++)
        {
            int index = _products.IndexOf(recipe.productTypes[i]);
            _notReservedProductsAmount[index] -= recipe.resourcesNum[i];
        }
        return true;
    }


}
