using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PathDrawing : MonoBehaviour
{

    [SerializeField] GameObject _LineDrawerPrefab;
    private int _lineIndex = 0;
    private Color _pathColor;
    public void SetPathColor(Color color)
    {
        _pathColor = color;
    }
    public void DrawLine(List<Vector2Int> nodes)
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
        _lineIndex++;
    }
    public void HidePath()
    {
        //_lineRenderer.positionCount = 0;
    }
}
