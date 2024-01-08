using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TaskSO : ScriptableObject
{
    private const string REFERENCES = "References";

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

    [Title("References"), FoldoutGroup(REFERENCES)]
    public Transform taskPhoneObjectUI;
    [FoldoutGroup(REFERENCES)]
    public Transform taskSelectedUI;

    [Title("General Attributes")]
    public string taskName;
    [TextArea(4, 8)]
    public string taskDescription;
    [Range(1, 3)]
    public int taskDifficulty = 1;

    public taskTheme taskSpecialty;
}
