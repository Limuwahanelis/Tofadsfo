using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSpeed : MonoBehaviour
{
    public float Speed=>_speed*PauseSettings.TimeSpeed;
    [SerializeField] float _speed;
}
