using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RaycastFromCamera : MonoBehaviour
{
    [SerializeField] LayerMask _mask;
    Camera _cam;
    // Start is called before the first frame update
    void Start()
    {
        _cam=Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IInteractable Raycast(out Vector3 point,out float width)
    {
        point = Vector3.zero;
        width = 0;
        RaycastHit hit;
        Ray ray = _cam.ScreenPointToRay(HelperClass.MousePos);
        if (Physics.Raycast(ray, out hit,10f, _mask))
        {
            IInteractable tmp = hit.transform.GetComponent<IInteractable>();
            if (tmp!=null)
            {
                point=hit.transform.position;
                width = hit.transform.lossyScale.x;
                return tmp;
            }
        }
        else
        {
            return null;
        }
        return null;
    }
}
