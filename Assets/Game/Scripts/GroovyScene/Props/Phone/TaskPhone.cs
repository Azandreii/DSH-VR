using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TaskPhone : MonoBehaviour
{
    public static TaskPhone Instance;

    public event EventHandler OnPhoneGrabbed;

    [Header("References")]
    [SerializeField] private GameObject phoneCanvas;
    [SerializeField] private GameObject phoneVisual;
    [SerializeField] private Transform phoneHolder;
    private bool inPants;
    private bool timeLerp;
    private bool changedStateToPlaying = false;
    private float lerpTimer;
    private float lerpTimerNormalized;

    [Header("References")]
    [SerializeField] private float lerpSpeed = 3f;
    [SerializeField] private float lerpTimerMax = 1f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        TimeClockManager.Instance.OnDayFinished += TimeClockManager_OnDayFinished;
    }

    private void TimeClockManager_OnDayFinished(object sender, EventArgs e)
    {
        phoneCanvas.SetActive(false);
    }

    private void Update()
    {
        if (timeLerp)
        {
            lerpTimer -= Time.deltaTime;
            lerpTimerNormalized = 1 - lerpTimer / lerpTimerMax;
            transform.position = Vector3.Lerp(transform.position, phoneHolder.transform.position, lerpSpeed * lerpTimerNormalized);
            transform.rotation = Quaternion.Lerp(transform.rotation, phoneHolder.transform.rotation, lerpSpeed * lerpTimerNormalized * 1.3f);
            if (lerpTimer <= 0)
            {
                timeLerp = false;
            }
        }
    }

    public void PhoneGrabbed()
    {
        OnPhoneGrabbed?.Invoke(this, EventArgs.Empty);
        GetComponent<Rigidbody>().isKinematic = true;
        PhoneManager.Instance.ShowMainMenu();
        if (!GameStateManager.Instance.GetIsTutorial() && !changedStateToPlaying)
        {
            GameStateManager.Instance.SetGamestate(GameStateManager.GameState.Playing);
            changedStateToPlaying = true;
        }
        else
        {
            changedStateToPlaying = true;
        }
    }

    public void IsNotInPants()
    {
        if (!GameStateManager.Instance.IsGameOver())
        {
            inPants = false;
            phoneCanvas.SetActive(true);
        }
    }

    public void IsInPants() 
    {
        inPants = true;
        phoneCanvas.SetActive(false);
    }

    public void BringPhoneToHolder()
    {
        if (!inPants)
        {
            lerpTimer = lerpTimerMax;
            timeLerp = true;
        }
    }

    public bool GetInPants()
    {
        return inPants;
    }
}
