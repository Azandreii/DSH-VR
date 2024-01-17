using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    const string MAIN_MENU = "MainMenu";
    const string LEVEL_DSH = "GroovyScene";
    const string LEVEL_POLICE = "Needs to be created";
    const string LEVEL_UMCG = "Needs to be created";
    const string LEVEL_DUO = "Needs to be created";

    public static SceneLoader Instance;

    [Header("References")]
    [SerializeField] private GameObject fadeQuad;

    [Header("Attributes")]
    [SerializeField] private bool fadeIn;
    [SerializeField] private bool fadeOut;
    private LevelScenes goToLevelScene = LevelScenes.MainMenu;
    private bool isChanging;

    [SerializeField] private float timeToFade;

    public enum LevelScenes
    {
        MainMenu,
        LevelDSH,
        LevelPolice,
        LevelUMCG,
        LevelDUO,
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (fadeIn)
        {
            isChanging = true;
        }
        else if (fadeOut)
        {
            isChanging = true;
        }
    }

    public void SetScene(LevelScenes _level)
    {
        switch (_level)
        {
            case LevelScenes.MainMenu:
                Debug.Log(MAIN_MENU);
                SceneManager.LoadScene(MAIN_MENU);
                break;
            case LevelScenes.LevelDSH:
                Debug.Log(LEVEL_DSH);
                SceneManager.LoadScene(LEVEL_DSH);
                break;
            case LevelScenes.LevelPolice:
                Debug.Log(LEVEL_POLICE);
                SceneManager.LoadScene(LEVEL_POLICE);
                break;
            case LevelScenes.LevelUMCG:
                Debug.Log(LEVEL_UMCG);
                SceneManager.LoadScene(LEVEL_UMCG);
                break;
            case LevelScenes.LevelDUO:
                Debug.Log(LEVEL_DUO);
                SceneManager.LoadScene(LEVEL_DUO);
                break;
        }
    }

    private void Update()
    {
        if (fadeIn && fadeQuad.alpha < 1)
        {
            fadeQuad.alpha += timeToFade * Time.deltaTime;
            if (fadeQuad.alpha >= 1)
            {
                fadeIn = false;
                isChanging = false;
                SetScene(goToLevelScene);
            }
        }
        if (fadeOut && fadeQuad.alpha >= 0)
        {
            fadeQuad.alpha -= timeToFade * Time.deltaTime;
            if (fadeQuad.alpha <= 0)
            {
                fadeOut = false;
                isChanging = false;
            }
        }
    }

    public void FadeIn()
    {
        if (!isChanging)
        {
            isChanging = true;
            fadeIn = true;
        }
    }

    public void FadeOut()
    {
        if (!isChanging)
        {  
            isChanging = true;
            fadeOut = true;
        }
    }

    public void ChangeScene(LevelScenes _level)
    {
        if (!isChanging)
        {
            goToLevelScene = _level;
        }
        FadeIn();
    }
}
