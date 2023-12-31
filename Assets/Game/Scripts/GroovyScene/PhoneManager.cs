using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject containerTaskUI;
    [SerializeField] private GameObject containerPhoneMenu;
    [SerializeField] private GameObject containerInternInformation;
    [SerializeField] private ButtonVR homeButtonTaskUI;
    [SerializeField] private ButtonVR homeButtonInternInformation;
    [SerializeField] private ButtonVR phoneMenuToTaskUI;
    [SerializeField] private ButtonVR phoneMenuToInternInformation;
    [SerializeField] private ButtonVR phoneMenuToLevelSelect;
    [SerializeField] private TextMeshProUGUI taskCountPopUp;
    [SerializeField] private Transform phoneInternObjectUI;
    [SerializeField] private Transform internGroup;

    private void Awake()
    {
        phoneInternObjectUI.gameObject.SetActive(false);
    }

    private void Start()
    {
        homeButtonTaskUI.OnClick += HomeButtonTaskUI_OnClick;
        homeButtonInternInformation.OnClick += HomeButtonInternInformation_OnClick;
        phoneMenuToTaskUI.OnClick += PhoneMenuTaskUI_OnClick;
        phoneMenuToInternInformation.OnClick += PhoneMenuInternInformation_OnClick;
        phoneMenuToLevelSelect.OnClick += PhoneMenuLevelSelect_OnClick;
        InternSpawner.Instance.OnInternCreated += InternSpawner_OnInternCreated;
    }

    private void InternSpawner_OnInternCreated(object sender, InternSpawner.OnInternCreatedEventArgs e)
    {
        SetIntern(e);
    }

    private void PhoneMenuLevelSelect_OnClick(object sender, ButtonVR.OnClickEventArgs e)
    {
        if (e.clickState == ButtonVR.ClickState.ClickDown)
        {
            //Scene change logic here
        }
    }

    private void PhoneMenuInternInformation_OnClick(object sender, ButtonVR.OnClickEventArgs e)
    {
        if (e.clickState == ButtonVR.ClickState.ClickDown)
        {
            ShowInternInformation();
        }
    }

    private void PhoneMenuTaskUI_OnClick(object sender, ButtonVR.OnClickEventArgs e)
    {
        if (e.clickState == ButtonVR.ClickState.ClickDown)
        {
            ShowTaskUI();
        }
    }

    private void HomeButtonInternInformation_OnClick(object sender, ButtonVR.OnClickEventArgs e)
    {
        if (e.clickState == ButtonVR.ClickState.ClickDown)
        {
            Debug.Log("HomeButtonPressed");
            ShowMainMenu();
        }
    }

    private void HomeButtonTaskUI_OnClick(object sender, ButtonVR.OnClickEventArgs e)
    {
        if (e.clickState == ButtonVR.ClickState.ClickDown)
        {
            Debug.Log("HomeButtonPressed");
            ShowMainMenu();
        }
    }

    private void ShowMainMenu()
    {
        containerTaskUI.SetActive(false);
        containerPhoneMenu.SetActive(true);
        containerInternInformation.SetActive(false);
    }

    private void ShowInternInformation()
    {
        containerTaskUI.SetActive(false);
        containerPhoneMenu.SetActive(false);
        containerInternInformation.SetActive(true);
    }

    private void ShowTaskUI()
    {
        containerTaskUI.SetActive(true);
        containerPhoneMenu.SetActive(false);
        containerInternInformation.SetActive(false);
    }

    private void SetIntern(InternSpawner.OnInternCreatedEventArgs e)
    {
            Transform _phoneInternUI = Instantiate(phoneInternObjectUI, internGroup);
            PhoneInternObjectUI pioui = _phoneInternUI.GetComponent<PhoneInternObjectUI>();
            pioui.SetInternSO(e.internSO);
    }
}
