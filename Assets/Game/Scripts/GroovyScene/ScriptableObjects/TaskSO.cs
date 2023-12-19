using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TaskSO : ScriptableObject
{
    public Transform taskObjectUI;
    public string taskName;
    [TextArea(4, 8)]
    public string taskDescription;
    [Range(1, 3)]
    public int taskDifficulty = 1;
}
