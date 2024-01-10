using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        Communication,
        Teamwork,
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
    private float taskTimerEasy = 3f;
    private float taskTimerMedium = 5f;
    private float taskTimerHard = 8f;
    private float taskTimerExtreme = 13f;
    private float taskTimerImpossible = 21f;

    public taskTheme taskSpecialty;

    public float GetTaskDifficulty()
    {
        switch (taskDifficulty)
        {
            case 1:
                return taskTimerEasy;
            case 2:
                return taskTimerMedium;
            case 3:
                return taskTimerHard;
            case 4:
                return taskTimerExtreme;
            case 5:
                return taskTimerImpossible;
        }
        return taskDifficulty;
    }
}
