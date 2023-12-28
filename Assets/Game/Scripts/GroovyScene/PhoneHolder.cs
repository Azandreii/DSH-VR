using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneHolder : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject taskPhone;

    public void OnSelectEnter()
    {
        TaskPhone tp = taskPhone.GetComponent<TaskPhone>();
        tp.HideCanvas();
    }

    public void OnSelectExit()
    {
        TaskPhone tp = taskPhone.GetComponent<TaskPhone>();
        tp.ShowCanvas();
    }
}
