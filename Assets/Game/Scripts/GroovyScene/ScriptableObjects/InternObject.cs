using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class InternSO : ScriptableObject
{
    public Transform internObjectUI;
    public string internName;
    public float energyEfficiency = 1f;
    public float processEfficiency = 1f;
    public float startEnergy = 200f;
}
