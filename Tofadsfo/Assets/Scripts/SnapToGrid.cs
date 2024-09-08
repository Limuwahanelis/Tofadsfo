using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SnapToGrid : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
    }
}
