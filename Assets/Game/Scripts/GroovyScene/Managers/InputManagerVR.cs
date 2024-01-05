using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerVR : MonoBehaviour
{
    public static InputManagerVR Instance;

    public event EventHandler<OnTriggerEventArgs> OnTrigger;
    public class OnTriggerEventArgs : EventArgs
    {
        public bool leftTrigger;
    }

    [Header("References")]
    [SerializeField] private GameObject controlerLeft;
    [SerializeField] private GameObject controlerRight;
    [SerializeField] private GameObject cameraVR;
    [SerializeField] private GameObject stopItem;
    private XRIDefaultInputActions inputActionsVR;

    private void Awake()
    {
        Instance = this;
        inputActionsVR = new XRIDefaultInputActions();
        inputActionsVR.XRIRightHandInteraction.Enable();
        inputActionsVR.XRILeftHandInteraction.Enable();

        inputActionsVR.XRIRightHandInteraction.Activate.performed += RightTrigger_performed;
        inputActionsVR.XRILeftHandInteraction.Activate.performed += LeftTrigger_performed; ;
    }

    private void LeftTrigger_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnTrigger?.Invoke(this, new OnTriggerEventArgs
        {
            leftTrigger = true,
        });
    }

    private void RightTrigger_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnTrigger?.Invoke(this, new OnTriggerEventArgs
        {
            leftTrigger = false,
        });
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
