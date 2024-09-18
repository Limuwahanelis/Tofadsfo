using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorkersPathDisplay : MonoBehaviour
{
    [SerializeField] GameObject _workerPathDisplaySelectorPrefab;
    [SerializeField] GameObject _displayHolder;
    [SerializeField] List<WorkerNavigation> _workersNavs;
    private List<WorkerPathDisplaySelector> _pathSelectors= new List<WorkerPathDisplaySelector>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<_workersNavs.Count;i++) 
        {
            WorkerPathDisplaySelector selector= Instantiate(_workerPathDisplaySelectorPrefab, _displayHolder.transform).GetComponent<WorkerPathDisplaySelector>();
            selector.SetUP(i+1,_workersNavs[i].AssociatedAssembly.Shortrecipe.productTypes.ToList(), _workersNavs[i].PathDrawer);
            _pathSelectors.Add(selector);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    //public void SetUp()
}
