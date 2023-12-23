using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu()]
public class InternSO : ScriptableObject
{
    public Transform internObjectUI;
    public string internName;
    [Title("Attributes") ,Range(0, 300)]
    public float startEnergy = 200f;
    public float maxEnergy = 300f;
    
    [PropertyRange(0, "MaxPrEfficiency"), FoldoutGroup("Process Efficiency"), Title("Processing Power")]
    public float processEfficiency = 1f;
    [FoldoutGroup("Process Efficiency")]
    [SerializeField] private float MaxPrEfficiency = 10f;

    [PropertyRange(0, "MaxEnEfficiency"), FoldoutGroup("Energy Efficiency"), Title("Energy Efficiency")]
    public float energyEfficiency = 1f;
    [FoldoutGroup("Energy Efficiency")]
    [SerializeField] private float MaxEnEfficiency = 10f;

    [Title("Energy Efficiency in State")]
    [InfoBox("All the effeciency values in this box are relative to their state. If you use -, it will decrease, if you will +, it will increase."), FoldoutGroup("Energy Efficiency")]
    public float availableEfficiency = 15f;
    [FoldoutGroup("Energy Efficiency")]
    public float workingEfficiency = -25f;
    [FoldoutGroup("Energy Efficiency")]
    public float awaitApprovalEfficiency = -3f;
    [FoldoutGroup("Energy Efficiency")]
    public float unavailableEfficiency = 10f;
}
