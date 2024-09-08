using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] Transform _transformToFollow;
    [SerializeField] Transform _transformToParentToOnUnparent;
    [SerializeField] bool _unparentOnStart;
    [SerializeField] bool _XAxis;
    [SerializeField] bool _YAxis;
    [SerializeField] bool _ZAxis;
    [SerializeField] Vector3 _offset;
    private Vector3 _originalpos;
    void Awake()
    {
        _originalpos = transform.position;
        if(_unparentOnStart)
        { 
        transform.SetParent(_transformToParentToOnUnparent);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 target = _transformToFollow.position+_offset;
        if (!_XAxis) target.x=_originalpos.x+_offset.x;
        if (!_YAxis) target.y=_originalpos.y +_offset.y;
        if (!_ZAxis) target.z=_originalpos.z + _offset.z;
        transform.position = target;
    }
}
