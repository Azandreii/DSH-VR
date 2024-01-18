using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{
    public static PhoneManager Instance;

    public enum PhoneState
    {
        PhoneMenu,
        InternInformation,
        PhoneTasks,
    }

    [Header("References")]
    [SerializeField] private GameObject containerTaskUI;
    [SerializeField] private GameObject containerPhoneMenu;
    [SerializeField] private GameObject containerInternInformation;
    [SerializeField] private GameObject containerPhoneClock;
    [SerializeField] private ButtonVR homeButtonTaskUI;
    [SerializeField] private ButtonVR homeButtonInternInformation;
    [SerializeField] private ButtonVR phoneMenuToTaskUI;
    [SerializeField] private ButtonVR phoneMenuToInternInformation;
    [SerializeField] private ButtonVR phoneMenuToLevelSelect;
    [SerializeField] private Transform phoneInternObjectUI;
    [SerializeField] private Transform internGroup;
    private PhoneState phoneState;

    private void Awake()
    {
        Instance = this;
        phoneInternObjectUI.gameObject.SetActive(false);
        containerInternInformation.gameObject.SetActive(false);
        containerPhoneMenu.gameObject.SetActive(false);
        containerTaskUI.gameObject.SetActive(false);
        containerPhoneClock.SetActive(false);
        phoneState = PhoneState.PhoneMenu;
    }

    private void Start()
    {
        homeButtonTaskUI.OnClick += HomeButtonTaskUI_OnClick;
        homeButtonInternInformation.OnClick += HomeButtonInternInformation_OnClick;
        phoneMenuToTaskUI.OnClick += PhoneMenuTaskUI_OnClick;
        phoneMenuToInternInformation.OnClick += PhoneMenuInternInformation_OnClick;
        phoneMenuToLevelSelect.OnClick += PhoneMenuLevelSelect_OnClick;
        if (InternSpawnerObject.Instance != null)
        {
            InternSpawnerObject.Instance.OnInternObjectCreated += InternSpawnerObject_OnInternCreated;
        }
    }

    private void InternSpawnerObject_OnInternCreated(object sender, InternSpawnerObject.OnInternObjectCreatedEventArgs e)
    {
        SetInternSO(e.internSO);
    }

    private void PhoneMenuLevelSelect_OnClick(object sender, ButtonVR.OnClickEventArgs e)
    {
        if (e.clickState == ButtonVR.ClickState.ClickDown)
        {
            //Scene change logic here
            SceneLoader.Instance.ChangeScene(SceneLoader.LevelScenes.MainMenu);
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
            ShowMainMenu();
        }
    }

    private void HomeButtonTaskUI_OnClick(object sender, ButtonVR.OnClickEventArgs e)
    {
        if (e.clickState == ButtonVR.ClickState.ClickDown)
        {
            ShowMainMenu();
        }
    }

    public void ShowMainMenu()
    {
        containerTaskUI.SetActive(false);
        containerPhoneMenu.SetActive(true);
        containerInternInformation.SetActive(false);
        phoneState = PhoneState.PhoneMenu;
        containerPhoneClock.SetActive(true);
        PhoneTaskCount.Instance.UpdateVisual();
    }

    private void ShowInternInformation()
    {
        containerTaskUI.SetActive(false);
        containerPhoneMenu.SetActive(false);
        containerInternInformation.SetActive(true);
        phoneState = PhoneState.InternInformation;
        containerPhoneClock.SetActive(true);
    }

    private void ShowTaskUI()
    {
        containerTaskUI.SetActive(true);
        containerPhoneMenu.SetActive(false);
        containerInternInformation.SetActive(false);
        phoneState = PhoneState.PhoneTasks;
        containerPhoneClock.SetActive(true);
;    }

    public void SetInternSO(InternSO _internSO)
    {
        Transform _phoneInternObjectUI = Instantiate(phoneInternObjectUI, internGroup);
        PhoneInternObjectUI _pioui = _phoneInternObjectUI.GetComponent<PhoneInternObjectUI>();
        _pioui.SetInternSO(_internSO);
        _phoneInternObjectUI.gameObject.SetActive(true);
    }

    public bool IsPhoneMenu()
    {
        return phoneState == PhoneState.PhoneMenu;
    }
}
