using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndManager : MonoBehaviour
{
    [SerializeField] List<WorkerController> _workers = new List<WorkerController>();
    private int _notWorkingWorkerCount = 0;
    private void Start()
    {
        for(int i=0;i<_workers.Count;i++)
        {
            _workers[i].OnWorkerNoIngredients += IncreaseNotWorkingWorkers;

        }
    }
    private void IncreaseNotWorkingWorkers()
    {
        _notWorkingWorkerCount++;
        if(_notWorkingWorkerCount==_workers.Count)
        {
            Logger.Log("Level end");
        }
    }
    private void OnDestroy()
    {
        for (int i = 0; i < _workers.Count; i++)
        {
            _workers[i].OnWorkerNoIngredients -= IncreaseNotWorkingWorkers;
        }
    }
}
