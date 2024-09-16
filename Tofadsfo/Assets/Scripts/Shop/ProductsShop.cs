using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProductsShop : MonoBehaviour
{
    [SerializeField] MoneyInfo _moneyInfo;
    [SerializeField] ProductsStats _productsStats;
    [SerializeField] GameObject _cardHolder;
    [SerializeField] GameObject _productShopCardPrefab;
    List<ProductShopCard> _productCards= new List<ProductShopCard>();

    private void Awake()
    {
        //_productCards = _cardHolder.GetComponentsInChildren<ProductShopCard>().ToList();

        //foreach (ProductShopCard card in _productCards)
        //{
        //    card.OnProductBought += BuyProduct;
        //}
    }
    public void SetUpShop(LevelInfoSO levelInfo)
    {
        List<ProductSO> distinctProducts = GetAllDistinctProducts(levelInfo);
        for (int i=0;i< distinctProducts.Count;i++) 
        {
            ProductShopCard card =Instantiate(_productShopCardPrefab, _cardHolder.transform).GetComponent<ProductShopCard>();
            card.SetUp(distinctProducts[i]);
            _productCards.Add(card);
            card.OnProductBought += BuyProduct;
        }
    }
    private List<ProductSO> GetAllDistinctProducts(LevelInfoSO levelInfo)
    {
        List<ProductSO> products=new List<ProductSO>();
        for (int i = 0; i < levelInfo.Orders.Count; i++)
        {
            for (int j = 0; j < levelInfo.Orders[i].Ingredients.Count; j++)
            {
                if (products.Contains(levelInfo.Orders[i].Ingredients[j])) continue;
                else products.Add(levelInfo.Orders[i].Ingredients[j]);
            }
        }
        return products;
    }
    private void BuyProduct(ProductSO product,int price,int amount)
    {
        int newAmount = amount;
        if( _moneyInfo.CurrentMoney<price*amount)
        {
            for(int i=0;i<=amount;i++)
            {
                if(i* price > _moneyInfo.CurrentMoney)
                {
                    i--;
                    newAmount = i;
                    break;
                }
            }
        }
        _moneyInfo.ReduceMoney(price* newAmount);
        _productsStats.OnIcreaseProductAmount(product, newAmount);
    }
    private void OnDestroy()
    {
        foreach (ProductShopCard card in _productCards)
        {
            card.OnProductBought -= BuyProduct;
        }
    }
}
