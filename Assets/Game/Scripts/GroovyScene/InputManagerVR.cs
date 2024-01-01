using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerVR : MonoBehaviour
{
    public static InputManagerVR Instance;

    [Header("References")]
    [SerializeField] private GameObject controlerLeft;
    [SerializeField] private GameObject controlerRight;
    [SerializeField] private GameObject cameraVR;
    [SerializeField] private GameObject stopItem;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject GetLeftControler()
    {
        return controlerLeft;
    }

    public GameObject GetRightControler()
    {
        return controlerRight;
    }

    public GameObject GetCamera()
    {
        return cameraVR;
    }

    public GameObject GetStopItem()
    {
        return stopItem;
    }
    public void OnRaySelectedEnterVR()
    {
        Debug.Log("OnRaySelectedEnterVR triggered");
    }

    public void OnRaySelectedExitVR()
    {
        Debug.Log("OnRaySelectedExitVR triggered");
    }
}
