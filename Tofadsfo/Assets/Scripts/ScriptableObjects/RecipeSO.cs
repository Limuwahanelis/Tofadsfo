using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New recipe",menuName ="Recipe")]
public class RecipeSO : ScriptableObject
{
    public ProductSO Result => _result;
    public List<ProductSO> Ingredients => _ingredients;
    public Sprite Icon => _result.Icon;
    [SerializeField] ProductSO _result;
    [SerializeField] List<ProductSO> _ingredients;
}
