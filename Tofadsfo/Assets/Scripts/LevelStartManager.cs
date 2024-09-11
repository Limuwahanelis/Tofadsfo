using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartManager : MonoBehaviour
{
    [SerializeField] LevelInfoSO _levelInfo;
    [SerializeField] LevelInfoDisplay _levelInfoDisplay;
    [SerializeField] PlayerMouseInteractions _mouseInteractions;
    private void Start()
    {
        _mouseInteractions.SetInteraction(false);
        _levelInfoDisplay.SetOrders(_levelInfo);
        _levelInfoDisplay.Show();
    }
}
