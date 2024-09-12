using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New product",menuName ="Product")]
public class ProductSO : ScriptableObject
{
    public int ShopPrice => _shopPrice;
    public Sprite Icon=>_icon;
    public int MaxAmount=>_maxAmount;
    public Color PathColor => _pathColor;
    [SerializeField] int _shopPrice;
    [SerializeField] Sprite _icon;
    [SerializeField] int _maxAmount;
    [SerializeField] Color _pathColor;
}
