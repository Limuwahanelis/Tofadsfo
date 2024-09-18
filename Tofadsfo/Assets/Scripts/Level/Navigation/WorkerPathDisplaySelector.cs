using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorkerPathDisplaySelector : MonoBehaviour
{

    [SerializeField] GameObject _pathTogglePrefab;
    [SerializeField] PathDrawing _pathDrawing;
    [SerializeField] GameObject _toggleHolder;
    [SerializeField] TMP_Text _workerIndexText;
    private List<WorkerPathDisplayToggle> _toggles=new List<WorkerPathDisplayToggle>();
    bool _showPathForAssembler;
    bool _showPathForRegister;
    bool _showPathForWorker;
    int index = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetUP(int index, List<ProductSO> products, PathDrawing pathDrawing)
    {
        _pathDrawing = pathDrawing;
        _workerIndexText.text =index.ToString();
        SpawnToggles(products);
    }
    public void SetShowPathsForProduct(bool value,ProductSO product)
    {

        if (value)
        {
            _pathDrawing.ShowPathsForProductFromAssembler(product);
            _pathDrawing.ShowPathsForProductFromRegister(product);
        }
        else
        {
            _pathDrawing.HidePathsForProductFromAssembler(product);
            _pathDrawing.HidePathsForProductFromRegister(product);
        }

    }
    public void SpawnToggles(List<ProductSO> products)
    {
        for (int i = 0; i < products.Count; i++) 
        {
            WorkerPathDisplayToggle toggle= Instantiate(_pathTogglePrefab, _toggleHolder.transform).GetComponent<WorkerPathDisplayToggle>();
            toggle.SetUp(products[i]);
            toggle.OnToggled += SetShowPathsForProduct;
            _toggles.Add(toggle);
        }
    }
    // used by toggle
    public void SetAssemblerPath(bool value)
    {
        _showPathForAssembler = value;
        if (_showPathForAssembler) _pathDrawing.ShowPathsForAssemlber();
        else _pathDrawing.HidePathsForAssembler();
    }
    // used by toggle
    public void SetRegisterPath(bool value)
    {
        _showPathForRegister = value;
        if (_showPathForRegister) _pathDrawing.ShowPathsForRegister();
        else _pathDrawing.HidePathsForRegister();
    }
    // used by toggle
    public void SetShowPathForWorker(bool value)
    {
        _showPathForWorker = value;
        _pathDrawing.gameObject.SetActive(value);
    }

    private void OnDestroy()
    {
        for(int i=0;i<_toggles.Count;i++)
        {
            _toggles[i].OnToggled -= SetShowPathsForProduct;
        }
    }
}
