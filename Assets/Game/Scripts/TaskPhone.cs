using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TaskPhone : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject phoneCanvas;
    [SerializeField] private GameObject phoneVisual;
    [SerializeField] private Transform phoneHolder;
    private bool inPants;
    private bool timeLerp;
    private float lerpTimer;
    private float lerpTimerNormalized;

    [Header("References")]
    [SerializeField] private float lerpSpeed = 3f;
    [SerializeField] private float lerpTimerMax = 1f;

    private void Update()
    {
        if (timeLerp)
        {
            lerpTimer -= Time.deltaTime;
            lerpTimerNormalized = 1 - lerpTimer / lerpTimerMax;
            transform.position = Vector3.Lerp(transform.position, phoneHolder.transform.position, lerpSpeed * lerpTimerNormalized);
            transform.rotation = Quaternion.Lerp(transform.rotation, phoneHolder.transform.rotation, lerpSpeed * lerpTimerNormalized * 1.3f);
            Debug.Log(lerpSpeed);
            if (lerpTimer <= 0)
            {
                timeLerp = false;
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }

    public void IsNotInPants()
    {
        inPants = false;
        phoneCanvas.SetActive(true);
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
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            lerpTimer = lerpTimerMax;
            timeLerp = true;
        }
    }
}
