using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WorkerNavigation : MonoBehaviour
{
     Register _workerRegister;
     RecipeAssembly _associateAssembler;
    [SerializeField] PathDrawing _pathDraw;
    [SerializeField] Transform _workerRegisterAccessPoint;
    [SerializeField] Transform _assemblyAccessPoint;
    [SerializeField] TableProductsManagment _productManagment;
    [SerializeField] AStarPathfinding _pathfinding;
    [SerializeField] List<ProductSO> _productsOrder;
    [SerializeField] List<Transform> _obstacles = new List<Transform>();
    private List<Vector2Int> _pathFromRegisterToAsembler;
    private List<PathWithProduct> _pathsFromRegisterToIngredients= new List<PathWithProduct>();
    private List<PathWithProduct> _pathsFromAssemblerToIngredients = new List<PathWithProduct>();
    private List<Color> _registerPathColors=new List<Color>();
    private List<Color> _assemblerPathColors = new List<Color>();

    public struct PathWithProduct
    {
        public TableWithProducts table;
        public ProductSO product;
        public List<Vector2Int> path;
    }
    private void Start()
    {
        _pathfinding.SetObstacles(TransToListInt(_obstacles));
        _pathfinding.DivideMapIntoTiles();
        SetPathFromRegisterToAssembler();
    }
    public void AssignRegisterAndAssembler(Register register,RecipeAssembly assemlber)
    {
        _workerRegister = register;
        _associateAssembler = assemlber;
    }
    public void DrawPaths()
    {
        //_pathDraw.SetPathColor(Color.white);
        //_pathDraw.DrawLine(_pathFromRegisterToAsembler);
        for (int i = 0; i < _pathsFromRegisterToIngredients.Count; i++)
        {
            _pathDraw.SetPathColor(_registerPathColors[i]);
            _pathDraw.DrawLine(_pathsFromRegisterToIngredients[i].path);
        }
        for (int i = 0; i < _pathsFromAssemblerToIngredients.Count; i++)
        {
            _pathDraw.SetPathColor(_assemblerPathColors[i]);
            _pathDraw.DrawLine(_pathsFromAssemblerToIngredients[i].path);
        }
    }
    public void AssignPaths()
    {

        //SetPathFromRegisterToAssembler();
        //SetPathsFromRegisteToIngredients();
        //SetPathsFromAssemblerToIngredients();
        _pathDraw.SetPathColor(Color.white);
        _pathDraw.DrawLine(_pathFromRegisterToAsembler);
        for(int i=0;i<_pathsFromRegisterToIngredients.Count;i++) 
        {
            _pathDraw.SetPathColor(_registerPathColors[i]);
            _pathDraw.DrawLine(_pathsFromRegisterToIngredients[i].path);
        }
        for(int i=0;i<_pathsFromAssemblerToIngredients.Count;i++)
        {
            _pathDraw.SetPathColor(_assemblerPathColors[i]);
            _pathDraw.DrawLine(_pathsFromAssemblerToIngredients[i].path);
        }
    }
    #region GetPaths
    /// <summary>
    /// 
    /// </summary>
    /// <param name="product"></param>
    /// <param name="table">Table with product</param>
    /// <returns></returns>
    public List<Vector2Int> GetShortestPathFromRegisterToProduct(ProductSO product,out TableWithProducts table)
    {
        List< PathWithProduct> paths = _pathsFromRegisterToIngredients.Where(x=>x.product==product).ToList();
        paths = paths.Where(x => x.table.CurrentProductAmount > 0).ToList();
        if (paths.Count == 0) Logger.Error("NO matching path");
        PathWithProduct shortestPOath = GetShortestPath(paths);

        table = shortestPOath.table;
        //if(table.CurrentProductAmount==1) _pathsFromRegisterToIngredients.Remove(shortestPOath);

        return shortestPOath.path;
    }
    public List<Vector2Int> GetShortestPathFromAssemblerToProduct(ProductSO product, out TableWithProducts table)
    {
        List<PathWithProduct> paths = _pathsFromAssemblerToIngredients.Where(x => x.product == product).ToList();
        paths = paths.Where(x => x.table.CurrentProductAmount > 0).ToList();
        if (paths.Count == 0) Logger.Error("NO matching path");
        PathWithProduct path=GetShortestPath(paths);
        table =path.table;
        //if (table.CurrentProductAmount == 1) _pathsFromAssemblerToIngredients.Remove(path);
        return path.path;
    }
    public List<Vector2Int> GetPathFromTableToAssembler(TableWithProducts table)
    {
        List<Vector2Int> path =new List<Vector2Int>( _pathsFromAssemblerToIngredients.Find(x => x.table == table).path);
        path.Reverse();
        return path;
    }
    public List<Vector2Int> GetPathFromAssemblerToregister()
    {
        List<Vector2Int> path =new List<Vector2Int>( _pathFromRegisterToAsembler);
        path.Reverse();
        return path;
    }
    private PathWithProduct GetShortestPath(List<PathWithProduct> paths)
    {
        PathWithProduct shortestPOath = paths[0];
        for (int i = 0; i < paths.Count; i++)
        {
            if (paths[i].path.Count < shortestPOath.path.Count) shortestPOath = paths[i];
        }
        return shortestPOath;
    }
    #endregion
    #region Make paths
    public void SetPathsForTable(TableWithProducts table)
    {

        PathWithProduct path = _pathsFromAssemblerToIngredients.Find(x => x.table == table);
        if (path.table != null)
        {
            int pathIndex = _pathsFromAssemblerToIngredients.IndexOf(path);
            _pathsFromAssemblerToIngredients.Remove(path);
            _assemblerPathColors.RemoveAt(pathIndex);
        }
        path = _pathsFromRegisterToIngredients.Find(x => x.table == table);
        if (path.table != null)
        {
            int pathIndex = _pathsFromRegisterToIngredients.IndexOf(path);
            _pathsFromRegisterToIngredients.Remove(path);
            _registerPathColors.RemoveAt(pathIndex);
        }
        bool haAny = false;
        for (int i = 0; i < _associateAssembler.Shortrecipe.productTypes.Count(); i++)
        {
            if (_associateAssembler.Shortrecipe.productTypes[i] == table.AssociatedProdct) haAny = true;
        }
        if (!haAny) return;
        int productIndex =_associateAssembler.Shortrecipe.productTypes.ToList().IndexOf(table.AssociatedProdct);

        _assemblerPathColors.Add(_associateAssembler.Shortrecipe.productTypes[productIndex].PathColor);
        _pathfinding.SetTargetTiles(TransformToVector2Int(_assemblyAccessPoint), TransformToVector2Int(table.AccessPoints[0]));
        _pathsFromAssemblerToIngredients.Add(new PathWithProduct()
        {
            path = _pathfinding.GetPath(),
            table = table,
            product = _associateAssembler.Shortrecipe.productTypes[productIndex],
        });

        _registerPathColors.Add(_associateAssembler.Shortrecipe.productTypes[productIndex].PathColor);
        _pathfinding.SetTargetTiles(TransformToVector2Int(_workerRegisterAccessPoint), TransformToVector2Int(table.AccessPoints[0]));
        _pathsFromRegisterToIngredients.Add(new PathWithProduct()
        {
            table = table,
            path = _pathfinding.GetPath(),
            product = _associateAssembler.Shortrecipe.productTypes[productIndex],
        });
    }
    //private void SetPathsFromAssemblerToIngredients()
    //{
    //    for (int i = 0; i < _productsOrder.Count; i++)
    //    {
    //        List<TableWithProducts> tables = _productManagment.GetAllTablesWithProduct(_productsOrder[i]);
    //        for (int j = 0; j < tables.Count; j++)
    //        {
    //            _assemblerPathColors.Add(_productsOrder[i].PathColor);
    //            _pathfinding.SetTargetTiles(TransformToVector2Int(_assemblyAccessPoint), TransformToVector2Int(tables[j].AccessPoints[0]));
    //            _pathsFromAssemblerToIngredients.Add(new PathWithProduct()
    //            {
    //                path = _pathfinding.GetPath(),
    //                table = tables[j],
    //                product = _productsOrder[i],
    //            });
    //        }
    //    }
    //}
    private void SetPathFromRegisterToAssembler()
    {
         _pathfinding.SetTargetTiles(TransformToVector2Int(_workerRegisterAccessPoint), TransformToVector2Int(_assemblyAccessPoint));
        _pathFromRegisterToAsembler =_pathfinding.GetPath();
    }
    //private void SetPathsFromRegisteToIngredients()
    //{
    //    for(int i=0;i<_productsOrder.Count;i++) 
    //    {
    //        List<TableWithProducts> tables = _productManagment.GetAllTablesWithProduct(_productsOrder[i]);
    //        for (int j = 0; j < tables.Count;j++)
    //        {
    //            _registerPathColors.Add(_productsOrder[i].PathColor);
    //            _pathfinding.SetTargetTiles(TransformToVector2Int(_workerRegisterAccessPoint), TransformToVector2Int(tables[j].AccessPoints[0]));
    //            _pathsFromRegisterToIngredients.Add(new PathWithProduct()
    //            {
    //                table = tables[j],
    //                path = _pathfinding.GetPath(),
    //                product = _productsOrder[i],
    //            });
                    
    //        }
    //    }
    //}
    #endregion
    #region Helper Methods
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
    #endregion
}
