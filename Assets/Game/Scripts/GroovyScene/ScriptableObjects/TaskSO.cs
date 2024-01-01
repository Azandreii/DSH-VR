using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TaskSO : ScriptableObject
{
    public enum taskTheme
    {
        Tech,
        Art,
        Design,
        Economy,
        Organisation,
        Research,
        Default,
    }

    public Transform taskObjectUI;
    public string taskName;
    [TextArea(4, 8)]
    public string taskDescription;
    [Range(1, 3)]
    public int taskDifficulty = 1;

    public taskTheme taskSpecialty;
}
