using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TabkleWithProductsTutorial : TableWithProducts
{
    public Action<TabkleWithProductsTutorial, ProductSO> OnSetProductOnTable;

    public override void SetProduct(ProductSO product)
    {
        base.SetProduct(product);
        OnSetProductOnTable?.Invoke(this, product);
    }
}
