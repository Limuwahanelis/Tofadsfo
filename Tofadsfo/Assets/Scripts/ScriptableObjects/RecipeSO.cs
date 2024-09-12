using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New recipe",menuName ="Recipe")]
public class RecipeSO : ScriptableObject
{
    public struct CraftingRecipeShort
    {
        public ProductSO[] productTypes;
        public int[] resourcesNum;
    }
    public int Price => _price;
    public ProductSO Result => _result;
    public List<ProductSO> Ingredients => _ingredients;
    public Sprite Icon => _result.Icon;
    [SerializeField] ProductSO _result;
    [SerializeField] List<ProductSO> _ingredients;
    [SerializeField] int _price;
}
