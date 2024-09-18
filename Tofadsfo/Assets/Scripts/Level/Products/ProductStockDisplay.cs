using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductStockDisplay : MonoBehaviour
{
    [SerializeField] GameObject _productStockHUDPrefab;
    [SerializeField] GameObject _productStockHUDHolder;
    public void SetUp(LevelInfoSO levelInfo,ProductsStats stats)
    {
        
        for (int i = 0; i < levelInfo.AvailableProducts.Count; i++)
        {
            ProductStockHUD stockDisplay = Instantiate(_productStockHUDPrefab, _productStockHUDHolder.transform).GetComponent<ProductStockHUD>();
            stockDisplay.SetUp(levelInfo.AvailableProducts[i], stats);
        }
        //RecipeSO.CraftingRecipeShort recipe = ConvertRecipetoShortFormat(levelInfo.Orders[i]);

    }
}
