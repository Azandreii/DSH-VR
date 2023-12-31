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

    [Header("References")]
    [SerializeField] private float lerpSpeed = 3f;

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
            Debug.Log(lerpSpeed);
            while (transform.position != phoneHolder.transform.position)
            {
                transform.position = Vector3.Lerp(transform.position, phoneHolder.transform.position, lerpSpeed);
            }
        }
    }
}
