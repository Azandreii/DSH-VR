using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskPhone : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject taskCanvas;
    [SerializeField] private GameObject phoneVisual;

    public void ShowCanvas()
    {
        taskCanvas.SetActive(true);
    }

    public void HideCanvas()
    {
        taskCanvas.SetActive(false);
    }
}
