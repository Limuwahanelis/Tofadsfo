using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New product",menuName ="Product")]
public class ProductSO : ScriptableObject
{
    public Sprite Icon=>_icon;
    public int MaxAmount=>_maxAmount;
    public int CurrentAmount => _currentAmount;
    [SerializeField] Sprite _icon;
    [SerializeField] int _maxAmount;
    [SerializeField] int _currentAmount;
}
