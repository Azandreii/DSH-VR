using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractHandsVR : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] InteractableObjectsVR;

    private void OnCollisionEnter(Collision _collision)
    {
        foreach (GameObject InteractableVR in InteractableObjectsVR)
        {
            if (_collision.gameObject == InteractableVR)
            {
                GameManager.Instance.TriggerInteractVR(_collision);
            }
        }
    }
}
