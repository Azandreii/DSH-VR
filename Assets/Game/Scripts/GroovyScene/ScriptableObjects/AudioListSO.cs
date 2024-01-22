using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioListSO : ScriptableObject
{
    public AudioSO taskCompleted;
    public AudioSO taskRecieved;
    public AudioSO internEnter;
    public AudioSO gameEnd;
    public AudioSO buttonPressed;
    public AudioSO fadeSound;
}
