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
    [SerializeField] private Color fadeColor;
    [SerializeField] private Renderer rend;
    [SerializeField] private bool fadeIn;
    [SerializeField] private bool fadeOut;
    [SerializeField] private float fadeDuration = 1f;
    private LevelScenes goToLevelScene = LevelScenes.MainMenu;
    private bool isChanging;
    private float timer;

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
        timer = 0;
    }

    private void SetScene(LevelScenes _level)
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
        if (fadeOut)
        {
            Color _newColor = fadeColor;
            _newColor.a = Mathf.Lerp(1, 0, timer / fadeDuration);

            rend.material.SetColor("_BaseColor", _newColor);

            timer += Time.deltaTime;

            if (timer / fadeDuration >= 1)
            {
                fadeOut = false;
                isChanging = false;
                timer = 0;
            }
        }
            if (fadeIn)
        {
            Color _newColor = fadeColor;
            _newColor.a = Mathf.Lerp(0, 1, timer / fadeDuration);

            rend.material.SetColor("_BaseColor", _newColor);

            timer += Time.deltaTime;
            
            if (timer / fadeDuration >= 1)
            {
                fadeIn = false;
                isChanging = false;
                SetScene(goToLevelScene);
                timer = 0;
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
