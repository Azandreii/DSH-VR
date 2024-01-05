using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PhoneInternObjectUI : MonoBehaviour
{
    [FoldoutGroup("PossibleSpecialtyStates")]
    public List<string> possibleSpecialties = new List<string>();
    private const string TECH_SELECTED = "selectedTech";
    private const string ART_SELECTED = "selectedArt";
    private const string DESIGN_SELECTED = "selectedDesign";
    private const string ECONOMY_SELECTED = "selectedEconomy";
    private const string ORGANISATION_SELECTED = "selectedOrganisation";
    private const string RESEARCH_SELECTED = "selectedResearch";

    [Header("References")]
    [SerializeField] private TextMeshProUGUI internName;
    [SerializeField] private TextMeshProUGUI internSpecialties;
    private InternSO internSO;

    private void Start()
    {
        possibleSpecialties.Clear();
        possibleSpecialties.Add(TECH_SELECTED);
        possibleSpecialties.Add(ART_SELECTED);
        possibleSpecialties.Add(DESIGN_SELECTED);
        possibleSpecialties.Add(ECONOMY_SELECTED);
        possibleSpecialties.Add(ORGANISATION_SELECTED);
        possibleSpecialties.Add(RESEARCH_SELECTED);
    }

    public void SetInternSO(InternSO _internSO)
    {
        internSO = _internSO;
        internName.text = _internSO.internName.ToString();
        GetInternSpecialties();
    }

    private void GetInternSpecialties()
    {
        //Intern's specialties that have been set in the Scriptable Object InternSO
        List<string> _selectedSpecialties = internSO.selectedSpecialties;

        //Starting the sentence for intern's specialties
        string _internSpecialtiesString = internSO.internName + "'s specialties are ";

        string _randomSkillOne = "flexibility";
        string _randomSkillTwo = "playing the guitar";
        string _randomSkillThree = "maths";
        List<string> _randomSkillList = new List<string>();
        _randomSkillList.Add(_randomSkillOne);
        _randomSkillList.Add(_randomSkillTwo);
        _randomSkillList.Add(_randomSkillThree);
        _internSpecialtiesString += _randomSkillList[UnityEngine.Random.Range(0, _randomSkillList.Count)];

        //Checking all the specialties that have been selected in the _selectedSpecialties
        //list and extending the sentence accordingly
        foreach (string _selectedSpecialty in _selectedSpecialties)
        {
            if (_selectedSpecialty == _selectedSpecialties[_selectedSpecialties.Count - 1])
            {
                //Will only be displayed at the last specialty in the list
                _internSpecialtiesString += " and ";
            }
            else
            {
                //Will be displayed on all the other specialties in the list
                _internSpecialtiesString += ", ";
            }
            switch (_selectedSpecialty)
            {
                case TECH_SELECTED:
                    //Code for showing that the intern has a specialty in tech
                    _internSpecialtiesString += "tech";
                    break;
                case ART_SELECTED:
                    //Code for showing that the intern has a specialty in art
                    _internSpecialtiesString += "art";
                    break;
                case DESIGN_SELECTED:
                    //Code for showing that the intern has a specialty in design
                    _internSpecialtiesString += "design";
                    break;
                case ECONOMY_SELECTED:
                    //Code for showing that the intern has a specialty in economy
                    _internSpecialtiesString += "economy";
                    break;
                case ORGANISATION_SELECTED:
                    //Code for showing that the intern has a specialty in organisation
                    _internSpecialtiesString += "organisation";
                    break;
                case RESEARCH_SELECTED:
                    //Code for showing that the intern has a specialty in research
                    _internSpecialtiesString += "research";
                    break;
            }
        }
        //Finishing the sentece to make it end more smoothly
        _internSpecialtiesString += ".";

        //Set the sentence in the phone for UI
        internSpecialties.text = _internSpecialtiesString;
        Show();
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
