using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskPhone : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject phoneCanvas;
    [SerializeField] private GameObject phoneVisual;

    public void ShowPhoneCanvas()
    {
        phoneCanvas.SetActive(true);
    }

    public void HidePhoneCanvas()
    {
        phoneCanvas.SetActive(false);
    }
}
