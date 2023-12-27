using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionVR : MonoBehaviour
{
    public event EventHandler<SelectedObjectsEventArgs> OnCollision;
    public class SelectedObjectsEventArgs : EventArgs
    {
        public GameObject collisionObject;
    }

    [Header("References")]
    [SerializeField] private GameObject[] InteractableObjectsVR;

    private void OnCollisionEnter(Collision _collision)
    {
        foreach (GameObject InteractableVR in InteractableObjectsVR)
        {
            if (_collision.gameObject == InteractableVR)
            {
                OnCollision?.Invoke(this, new SelectedObjectsEventArgs 
                {
                    collisionObject = InteractableVR,
                });
            }
        }
    }
}
