using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu()]
public class InternSO : ScriptableObject
{
    private const string TECH_SELECTED = "selectedTech";
    private const string ART_SELECTED = "selectedArt";
    private const string DESIGN_SELECTED = "selectedDesign";
    private const string ECONOMY_SELECTED = "selectedEconomy";
    private const string ORGANISATION_SELECTED = "selectedOrganisation";
    private const string RESEARCH_SELECTED = "selectedResearch";


    [System.Flags]
    public enum Proficiency
    {
        Tech = 1 << 1,
        Art = 1 << 2,
        Design = 1 << 3,
        Economy = 1 << 4,
        Organisation = 1 << 5,
        Research = 1 << 6,
        All = Tech | Art | Design | Economy | Organisation | Research
    }

    public Transform internObjectUI;
    public string internName;
    [Title("Attributes") ,Range(0, 300)]
    public float startEnergy = 200f;
    public float maxEnergy = 300f;

    [ButtonGroup("SelectedSpecialty")]
    private void Tech()
    {
        selectedTech = !selectedTech;
    }
    [ButtonGroup("SelectedSpecialty")]
    private void Art()
    {
        selectedArt = !selectedArt;
    }
    [ButtonGroup("SelectedSpecialty")]
    private void Design()
    {
        selectedDesign = !selectedDesign;
    }
    [ButtonGroup("SelectedSpecialty")]
    private void Economy()
    {
        selectedEconomy = !selectedEconomy;
    }
    [ButtonGroup("SelectedSpecialty")]
    private void Organisation()
    {
        selectedOrganisation = !selectedOrganisation;
    }
    [ButtonGroup("SelectedSpecialty")]
    private void Research()
    {
        selectedResearch = !selectedResearch;
    }

    [HideInInspector]
    public bool selectedTech = false;
    [HideInInspector]
    public bool selectedArt = false;
    [HideInInspector]
    public bool selectedDesign = false;
    [HideInInspector]
    public bool selectedEconomy = false;
    [HideInInspector]
    public bool selectedOrganisation = false;
    [HideInInspector]
    public bool selectedResearch = false;

    [PropertyRange(0, "MaxPrEfficiency"), FoldoutGroup("Process Efficiency"), Title("Processing Power")]
    public float processEfficiency = 1f;
    [FoldoutGroup("Process Efficiency")]
    [SerializeField] private float MaxPrEfficiency = 10f;

    [PropertyRange(0, "MaxEnEfficiency"), FoldoutGroup("Energy Efficiency"), Title("Energy Efficiency")]
    public float energyEfficiency = 1f;
    [FoldoutGroup("Energy Efficiency")]
    [SerializeField] private float MaxEnEfficiency = 10f;

    [Title("Energy Efficiency in State")]
    [InfoBox("All the effeciency values in this box are relative to their state. If you use -, it will decrease, if you use +, it will increase."), FoldoutGroup("Energy Efficiency")]
    public float availableEfficiency = 15f;
    [FoldoutGroup("Energy Efficiency")]
    public float workingEfficiency = -25f;
    [FoldoutGroup("Energy Efficiency")]
    public float awaitApprovalEfficiency = -3f;
    [FoldoutGroup("Energy Efficiency")]
    public float unavailableEfficiency = 10f;

    [ShowIf(TECH_SELECTED), FoldoutGroup("Specialty Group")]
    public float techEfficiency = 2f;
    [ShowIf(ART_SELECTED), FoldoutGroup("Specialty Group")]
    public float artEfficiency = 2f;
    [ShowIf(DESIGN_SELECTED), FoldoutGroup("Specialty Group")]
    public float designEfficiency = 2f;
    [ShowIf(ECONOMY_SELECTED), FoldoutGroup("Specialty Group")]
    public float economyEfficiency = 2f;
    [ShowIf(ORGANISATION_SELECTED), FoldoutGroup("Specialty Group")]
    public float organisationEfficiency = 2f;
    [ShowIf(RESEARCH_SELECTED), FoldoutGroup("Specialty Group")]
    public float researchEfficiency = 2f;
}
