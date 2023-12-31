using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PhoneInternObjectUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI internName;
    [SerializeField] private TextMeshProUGUI internSpecialties;
    private InternSO internSO;

    public void SetInternSO(InternSO _internSO)
    {
        internSO = _internSO;
        internName.text = _internSO.internName.ToString();
        GetInternSpecialties();
    }

    private void GetInternSpecialties()
    {
        string internSpecialtiesString = internSO.internName + "'s specialties are ";
        if (GetTechEfficiency())
        {
            internSpecialtiesString += "tech, ";
        }
        if (GetArtEfficiency())
        {
            internSpecialtiesString += "art, ";
        }
        if (GetDesignEfficiency())
        {
            internSpecialtiesString += "design, ";
        }
        if (GetEconomyEfficiency())
        {
            internSpecialtiesString += "economy, ";
        }
        if (GetOrganisationEfficiency())
        {
            internSpecialtiesString += "organisation, ";
        }
        if (GetResearchEfficiency())
        {
            internSpecialtiesString += "research, ";
        }
        internSpecialtiesString += "learning and flexibility.";
        internSpecialties.text = internSpecialtiesString;
        Show();
    }

    public bool GetTechEfficiency()
    {
        if (internSO.selectedTech)
        {
            return true;
        }
        return false;
    }

    public bool GetArtEfficiency()
    {
        if (internSO.selectedArt)
        {
            return true;
        }
        return false;
    }

    public bool GetDesignEfficiency()
    {
        if (internSO.selectedDesign)
        {
            return true;
        }
        return false;
    }

    public bool GetEconomyEfficiency()
    {
        if (internSO.selectedEconomy)
        {
            return true;
        }
        return false;
    }

    public bool GetOrganisationEfficiency()
    {
        if (internSO.selectedOrganisation)
        {
            return true;
        }
        return false;
    }

    public bool GetResearchEfficiency()
    {
        if (internSO.selectedResearch)
        {
            return true;
        }
        return false;
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject?.SetActive(false);
    }
}
