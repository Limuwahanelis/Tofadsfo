using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PathDrawing : MonoBehaviour
{
    struct LineToTable
    {
       public LineRenderer lineRenderer;
       public TableWithProducts table;
    }

    [SerializeField] GameObject _LineDrawerPrefab;
    [SerializeField] GameObject _assemblerLinesHolder;
    [SerializeField] GameObject _registerLineHolder;
    private List<LineRenderer> _lineRenderes=new List<LineRenderer>();
    private List<LineToTable> _linesFromAssemblerToProducts=new List<LineToTable>();
    private List<LineToTable> _linesFromRegisterToProducts=new List<LineToTable>();
    private List<ProductSO> _productsToShowLinesFor=new List<ProductSO>();
    private int _lineIndex = 0;
    private Color _pathColor;
    #region Assembler paths
    public void CreateLineFromAssemblerToTable(List<Vector2Int> nodes,TableWithProducts table)
    {
        LineRenderer lineRenderer = CreatePath(nodes);
        _linesFromAssemblerToProducts.Add(new LineToTable()
        {
            lineRenderer = lineRenderer,
            table = table
        });
        lineRenderer.transform.SetParent(_assemblerLinesHolder.transform);
        if (_productsToShowLinesFor.Contains(table.AssociatedProdct)) return;
        lineRenderer.enabled = false;
        
    }
    public void ShowPathsForProductFromAssembler(ProductSO product)
    {
        _productsToShowLinesFor.Add(product);
        List<LineToTable> lines = _linesFromAssemblerToProducts.Where(x => x.table.AssociatedProdct == product).ToList();
        foreach (LineToTable line in lines) 
        {
            line.lineRenderer.enabled = true;
        }
    }
    public void HidePathsForProductFromAssembler(ProductSO product)
    {
        _productsToShowLinesFor.Remove(product);
        List<LineToTable> lines = _linesFromAssemblerToProducts.Where(x => x.table.AssociatedProdct == product).ToList();
        foreach (LineToTable line in lines)
        {
            line.lineRenderer.enabled = false;
        }
    }
    public void ShowPathsForAssemlber()
    {
        _assemblerLinesHolder.SetActive(true);
    }
    public void HidePathsForAssembler()
    {
        _assemblerLinesHolder.SetActive(false);
    }
    public void RemovePathFromAssemblerToTable(TableWithProducts table)
    {
        LineToTable line= _linesFromAssemblerToProducts.Find(x => x.table == table);
        _linesFromAssemblerToProducts.Remove(line);
        Destroy(line.lineRenderer.gameObject);
    }
    #endregion
    #region Register paths
    public void CreateLineFromRegisterToTable(List<Vector2Int> nodes, TableWithProducts table)
    {
        LineRenderer lineRenderer = CreatePath(nodes);
        _linesFromRegisterToProducts.Add(new LineToTable()
        {
            lineRenderer = lineRenderer,
            table = table
        });
        lineRenderer.transform.SetParent(_registerLineHolder.transform);
        if (_productsToShowLinesFor.Contains(table.AssociatedProdct)) return;
        lineRenderer.enabled = false;

    }
    public void ShowPathsForProductFromRegister(ProductSO product)
    {
        _productsToShowLinesFor.Add(product);
        List<LineToTable> lines = _linesFromRegisterToProducts.Where(x => x.table.AssociatedProdct == product).ToList();
        foreach (LineToTable line in lines)
        {
            line.lineRenderer.enabled = true;
        }
    }
    public void HidePathsForProductFromRegister(ProductSO product)
    {
        _productsToShowLinesFor.Remove(product);
        List<LineToTable> lines = _linesFromRegisterToProducts.Where(x => x.table.AssociatedProdct == product).ToList();
        foreach (LineToTable line in lines)
        {
            line.lineRenderer.enabled = false;
        }
    }

    public void ShowPathsForRegister()
    {
        _registerLineHolder.SetActive(true);
    }
    public void HidePathsForRegister()
    {
        _registerLineHolder.SetActive(false);
    }
    public void RemovePathFromRegisterToTable(TableWithProducts table)
    {
        LineToTable line = _linesFromRegisterToProducts.Find(x => x.table == table);
        _linesFromRegisterToProducts.Remove(line);
        Destroy(line.lineRenderer.gameObject);
    }
    #endregion
    private LineRenderer CreatePath(List<Vector2Int> nodes)
    {
        LineRenderer lineRenderer = Instantiate(_LineDrawerPrefab).GetComponent<LineRenderer>();

        lineRenderer.positionCount = 0;
        lineRenderer.startColor = _pathColor;
        lineRenderer.endColor = _pathColor;
        lineRenderer.positionCount = nodes.Count;
        for (int i = 0; i < nodes.Count; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(nodes[i].x, nodes[i].y, -0.05f));
        }
        return lineRenderer;
    }
    public void SetPathColor(Color color)
    {
        _pathColor = color;
    }
    public void ClearPahts()
    {
        for(int i=0;i<_lineRenderes.Count;i++) 
        {
            Destroy(_lineRenderes[i].gameObject);
        }
        _lineRenderes.Clear();
    }
    public void DrawLine(List<Vector2Int> nodes)
    {
        LineRenderer lineRenderer = Instantiate(_LineDrawerPrefab).GetComponent<LineRenderer>();
        _lineRenderes.Add(lineRenderer);
        lineRenderer.positionCount = 0;
        lineRenderer.startColor = _pathColor;
        lineRenderer.endColor = _pathColor;
        lineRenderer.positionCount = nodes.Count;
        for (int i = 0; i < nodes.Count; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(nodes[i].x, nodes[i].y, -0.05f));
        }
        _lineIndex++;
    }
    public void HidePath()
    {
        //_lineRenderer.positionCount = 0;
    }
}
