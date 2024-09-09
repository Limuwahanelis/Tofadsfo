using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerNavigation : MonoBehaviour
{
    [SerializeField] PathDrawing _pathDraw;
    [SerializeField] Transform _workerRegisterAccessPoint;
    [SerializeField] Transform _assemblyAccessPoint;
    [SerializeField] TableProductsManagment _productManagment;
    [SerializeField] AStarPathfinding _pathfinding;
    [SerializeField] List<ProductSO> _productsOrder;
    [SerializeField] List<Transform> _obstacles = new List<Transform>();
    List<Vector2Int> _pathFromAssemblerToRegister;
    List<List<Vector2Int>> _pathsFromRegisterToIngredients= new List<List<Vector2Int>>();
    List<List<Vector2Int>> _pathsFromAssemblerToIngredients = new List<List<Vector2Int>>();
    List<Color> _registerPathColors=new List<Color>();
    List<Color> _assemblerPathColors = new List<Color>();
    public void AssignPaths()
    {
        _pathfinding.SetObstacles(TransToListInt(_obstacles));
        _pathfinding.DivideMapIntoTiles();
        SetPathFromRegisterToAssembler();
        SetPathsFromRegisteToIngredients();
        SetPathsFromAssemblerToIngredients();
        _pathDraw.SetPathColor(Color.white);
        _pathDraw.DrawLine(_pathFromAssemblerToRegister);
        for(int i=0;i<_pathsFromRegisterToIngredients.Count;i++) 
        {
            _pathDraw.SetPathColor(_registerPathColors[i]);
            _pathDraw.DrawLine(_pathsFromRegisterToIngredients[i]);
        }
        for(int i=0;i<_pathsFromAssemblerToIngredients.Count;i++)
        {
            _pathDraw.SetPathColor(_assemblerPathColors[i]);
            _pathDraw.DrawLine(_pathsFromAssemblerToIngredients[i]);
        }
    }
    private void SetPathsFromAssemblerToIngredients()
    {
        for (int i = 0; i < _productsOrder.Count; i++)
        {
            List<TableWithProducts> tables = _productManagment.GetAllTablesWithProduct(_productsOrder[i]);
            for (int j = 0; j < tables.Count; j++)
            {
                _assemblerPathColors.Add(_productsOrder[i].PathColor);
                _pathfinding.SetTargetTiles(TransformToVector2Int(_assemblyAccessPoint), TransformToVector2Int(tables[j].AccessPoints[0]));
                _pathsFromAssemblerToIngredients.Add(_pathfinding.GetPath());
            }
        }
    }
    private void SetPathFromRegisterToAssembler()
    {
         _pathfinding.SetTargetTiles(TransformToVector2Int(_workerRegisterAccessPoint), TransformToVector2Int(_assemblyAccessPoint));
        _pathFromAssemblerToRegister =_pathfinding.GetPath();
    }
    private void SetPathsFromRegisteToIngredients()
    {
        for(int i=0;i<_productsOrder.Count;i++) 
        {
            List<TableWithProducts> tables = _productManagment.GetAllTablesWithProduct(_productsOrder[i]);
            for (int j = 0; j < tables.Count;j++)
            {
                _registerPathColors.Add(_productsOrder[i].PathColor);
                _pathfinding.SetTargetTiles(TransformToVector2Int(_workerRegisterAccessPoint), TransformToVector2Int(tables[j].AccessPoints[0]));
                _pathsFromRegisterToIngredients.Add(_pathfinding.GetPath());
            }
        }
    }
    Vector2Int TransformToVector2Int(Transform tran)
    {
        return Vector3ToInt(tran.position);
    }
    Vector2Int Vector3ToInt(Vector3 vec)
    {
        Vector2Int toRet=Vector2Int.zero;
        toRet.x = ((int)vec.x);
        toRet.y = ((int)vec.y);
        return toRet;
    }
    List<Vector2Int> TransToListInt(List<Transform> trans)
    {
        List<Vector2Int> toRet = new List<Vector2Int>();
        for(int i=0;i< trans.Count;i++)
        {
            Vector2Int tmp = Vector2Int.zero;
            tmp.x = ((int)trans[i].position.x);
            tmp.y = ((int)trans[i].position.y);
            toRet.Add(tmp);
        }

        return toRet;
    }
}
