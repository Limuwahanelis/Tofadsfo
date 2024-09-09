using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeAssembly : MonoBehaviour
{
    public RecipeSO Recipe => _recipe;
    [SerializeField] RecipeSO _recipe;
    [SerializeField] SpriteRenderer _spriteRenderer;
    List<ProductSO> _collectedProducts=new List<ProductSO>();

    public void Assemble()
    {
        for(int i=0;i<_recipe.Ingredients.Count;i++)
        {
            if (!_collectedProducts.Contains(_recipe.Ingredients[i])) return;
        }
        Logger.Log("assem");
    }
    public void AddIngredient(ProductSO ingredient)
    {
        _collectedProducts.Add(ingredient);
    }
    private void OnValidate()
    {
        if(_recipe!=null)
        {
            _spriteRenderer.sprite=_recipe.Icon;
        }
    }
}
