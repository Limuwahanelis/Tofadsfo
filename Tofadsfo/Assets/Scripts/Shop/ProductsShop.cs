using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProductsShop : MonoBehaviour
{
    [SerializeField] MoneyInfo _moneyInfo;
    [SerializeField] ProductsStats _productsStats;
    [SerializeField] GameObject _cardHolder;
    List<ProductShopCard> _productCards= new List<ProductShopCard>();

    private void Awake()
    {
        _productCards = _cardHolder.GetComponentsInChildren<ProductShopCard>().ToList();

        foreach (ProductShopCard card in _productCards)
        {
            card.OnProductBought += BuyProduct;
        }
    }

    private void BuyProduct(ProductSO product,int price,int amount)
    {
        _moneyInfo.ReduceMoney(price*amount);
        _productsStats.OnIcreaseProductAmount(product, amount);
    }
}
