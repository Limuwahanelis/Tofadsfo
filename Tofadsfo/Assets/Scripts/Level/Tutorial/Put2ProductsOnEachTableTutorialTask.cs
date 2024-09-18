using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Put2ProductsOnEachTableTutorialTask : MonoBehaviour
{
    [SerializeField] ProductSO _NONEProduct;
    [SerializeField] TutorialEventSO _tutorialStep;
    [SerializeField] ProductSO _product1;
    [SerializeField] ProductSO _product2;
    [SerializeField] List<TabkleWithProductsTutorial> _tables = new List<TabkleWithProductsTutorial>();
    List<TabkleWithProductsTutorial> _tablesWithProduct1 = new List<TabkleWithProductsTutorial>();
    List<TabkleWithProductsTutorial> _tablesWithProduct2 = new List<TabkleWithProductsTutorial>();
    void Start()
    {
        for(int i=0;i<_tables.Count;i++) 
        {
            _tables[i].OnSetProductOnTable += CheckProducts;
        }
    }

    private void CheckProducts(TabkleWithProductsTutorial table,ProductSO product)
    {
        
        if(product== _NONEProduct)
        {
            if(_tablesWithProduct1.Contains(table)) _tablesWithProduct1.Remove(table);
            if(_tablesWithProduct2.Contains(table)) _tablesWithProduct2.Remove(table);
        }
        else if(product== _product1) 
        {
            if(_tablesWithProduct2.Contains(table)) _tablesWithProduct2.Remove(table);
            if(!_tablesWithProduct1.Contains(table))_tablesWithProduct1.Add(table);

            if(_tablesWithProduct1.Count==2 && _tablesWithProduct2.Count==2) 
            {
                CompleteStep();
            }
        }
        else if(product== _product2) 
        {
            if (_tablesWithProduct1.Contains(table)) _tablesWithProduct1.Remove(table);
            if (!_tablesWithProduct2.Contains(table)) _tablesWithProduct2.Add(table);

            if (_tablesWithProduct1.Count == 2 && _tablesWithProduct2.Count == 2)
            {
                CompleteStep();
            }
        }
    }

    private void CompleteStep()
    {
        for (int i = 0; i < _tables.Count; i++)
        {
            _tables[i].OnSetProductOnTable -= CheckProducts;
        }
        _tutorialStep.CompleteStep();
    }
}
