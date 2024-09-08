using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RaycastFromCamera : MonoBehaviour
{
    [SerializeField] LayerMask _mask;
    //RaycastHit2D[] hits ;
    Camera _cam;
    Vector2 _mousePos;
    // Start is called before the first frame update
    void Start()
    {
        _cam=Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TryPress()
    {
        RaycastHit hit;
        Ray ray = _cam.ScreenPointToRay(_mousePos);
        Logger.Log($"{ray.origin}");
        if (Physics.Raycast(ray, out hit,10f, _mask))
        {
            
            
            IInteractable tmp = hit.transform.GetComponent<IInteractable>();
            if (tmp!=null)
            {
                Logger.Log("FASF");
                tmp.Interact();
            }
        }
    }
    public void SetMousePos(Vector2 newMousePos)
    {
        _mousePos = newMousePos;
    }
}
