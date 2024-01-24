using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class OnCollisionVR : MonoBehaviour
{
    public event EventHandler OnCollisionControler;
    public event EventHandler<SelectedObjectsEventArgs> OnCollisionGameObject;
    public class SelectedObjectsEventArgs : EventArgs
    {
        public GameObject collisionObject;
    }

    [Header("References")]
    protected List<GameObject> controlersListVR;
    //[SerializeField] private GameObject[] interactableObjectsVR;

    protected bool hasControlers;

    private void Start()
    {
        SetControlerReferences();
    }

    protected void OnCollisionEnter(Collision _collision)
    {
        CheckColisionWithControler(_collision);
        ColisionWithObject(_collision);
    }

    protected void CheckColisionWithControler(Collision _collision)
    {
        foreach (GameObject _controlerVR in controlersListVR)
        {
            if (_collision.gameObject == _controlerVR)
            {
                OnCollisionControler?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    protected virtual void ColisionWithObject(Collision _collision)
    {
        OnCollisionGameObject?.Invoke(this, new SelectedObjectsEventArgs
        {
            collisionObject = _collision.gameObject,
        });
    }

    private void SetControlerReferences()
    {
        if (!hasControlers)
        {
            controlersListVR = new List<GameObject>();
            controlersListVR.Add(InputManagerVR.Instance.GetLeftControler());
            controlersListVR.Add(InputManagerVR.Instance.GetRightControler());
            hasControlers = true;
        }
    }
}
