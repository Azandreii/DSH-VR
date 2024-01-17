using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject containerMainMenu;
    [SerializeField] private GameObject containerPartnerSelect;
    [SerializeField] private Image background;
    [SerializeField] private ButtonVR buttonPartnerSelect;
    [SerializeField] private ButtonVR buttonQuit;
    [SerializeField] private ButtonVR buttonDSH;
    [SerializeField] private ButtonVR buttonPolice;
    [SerializeField] private ButtonVR buttonUMCG;
    [SerializeField] private ButtonVR buttonDUO;
    [SerializeField] private ButtonVR buttonReturnMainMenu;

    private void Awake()
    {
        containerMainMenu.SetActive(true);
        containerPartnerSelect.SetActive(false);
        background.gameObject.SetActive(true);
    }

    private void Start()
    {
        buttonPartnerSelect.OnClick += ButtonPartnerSelect_OnClick;
        buttonQuit.OnClick += ButtonQuit_OnClick;
        buttonDSH.OnClick += ButtonDSH_OnClick;
        buttonPolice.OnClick += ButtonPolice_OnClick;
        buttonUMCG.OnClick += ButtonUMCG_OnClick;
        buttonDUO.OnClick += ButtonDUO_OnClick;
        buttonReturnMainMenu.OnClick += ButtonReturnMainMenu_OnClick;
    }

    private void ButtonDUO_OnClick(object sender, ButtonVR.OnClickEventArgs e)
    {
        if (e.clickState == ButtonVR.ClickState.ClickDown)
        {
            SceneLoader.Instance.ChangeScene(SceneLoader.LevelScenes.LevelDUO);
        }
    }

    private void ButtonUMCG_OnClick(object sender, ButtonVR.OnClickEventArgs e)
    {
        if (e.clickState == ButtonVR.ClickState.ClickDown)
        {
            SceneLoader.Instance.ChangeScene(SceneLoader.LevelScenes.LevelUMCG);
        }
    }

    private void ButtonPolice_OnClick(object sender, ButtonVR.OnClickEventArgs e)
    {
        if (e.clickState == ButtonVR.ClickState.ClickDown)
        {
            SceneLoader.Instance.ChangeScene(SceneLoader.LevelScenes.LevelPolice);
        }
    }

    private void ButtonDSH_OnClick(object sender, ButtonVR.OnClickEventArgs e)
    {
        if (e.clickState == ButtonVR.ClickState.ClickDown)
        {
            SceneLoader.Instance.ChangeScene(SceneLoader.LevelScenes.LevelDSH);
        }
    }

    private void ButtonQuit_OnClick(object sender, ButtonVR.OnClickEventArgs e)
    {
        if (e.clickState == ButtonVR.ClickState.ClickDown)
        {
            Application.Quit();
            Debug.Log("Quit");
        }
    }

    private void ButtonReturnMainMenu_OnClick(object sender, ButtonVR.OnClickEventArgs e)
    {
        if (e.clickState == ButtonVR.ClickState.ClickDown)
        {
            containerMainMenu.SetActive(true);
            containerPartnerSelect.SetActive(false);
            background.gameObject.SetActive(true);
        }
    }

    private void ButtonPartnerSelect_OnClick(object sender, ButtonVR.OnClickEventArgs e)
    {
        if (e.clickState == ButtonVR.ClickState.ClickDown)
        {
            containerMainMenu.SetActive(false);
            containerPartnerSelect.SetActive(true);
            background.gameObject.SetActive(true);
        }
    }
}
