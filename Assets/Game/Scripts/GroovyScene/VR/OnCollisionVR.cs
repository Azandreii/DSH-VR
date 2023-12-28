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
    private List<GameObject> controlersListVR;
    //[SerializeField] private GameObject[] interactableObjectsVR;

    private bool hasControlers;

    private void Start()
    {
        SetControlerReferences();
    }

    private void OnCollisionEnter(Collision _collision)
    {
        CheckColisionWithControler(_collision);
        ColisionWithObject(_collision);
    }

    private void CheckColisionWithControler(Collision _collision)
    {
        foreach (GameObject ControlerVR in controlersListVR)
        {
            if (_collision.gameObject == ControlerVR)
            {
                OnCollisionControler?.Invoke(this, EventArgs.Empty);
                Debug.Log("Collision found between object and controler");
            }
        }
    }

    private void ColisionWithObject(Collision _collision)
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
            Debug.Log("Controlers set to collision");
        }
    }
}
