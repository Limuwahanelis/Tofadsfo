using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TableWithProducts : MonoBehaviour,IInteractable
{
    public void Interact()
    {
        ShowProductSelection();
    }


    public void ShowProductSelection()
    {
        Logger.Log("Pressed");
    }

}
