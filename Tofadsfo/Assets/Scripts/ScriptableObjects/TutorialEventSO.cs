using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New tutorial step",menuName ="Tutorial step")]
public class TutorialEventSO : ScriptableObject
{
    public Action OnStepCompleted;
    public void CompleteStep()
    {
            OnStepCompleted?.Invoke();
    }
}
