using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VisionFollower : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float distance = 3.0f;
    [SerializeField] private float positionSpeedModifier = 0.025f;
    [SerializeField] private float rotationSpeedModifier = 200f;

    private bool isCentered = false;

    private void OnBecameInvisible()
    {
        Debug.Log("GameEndUI is Invisible");
        if (GameStateManager.Instance.IsGameOver())
        {
            isCentered = false;
        }
    }

    private void Update()
    {
        if (isCentered)
        {
            Vector3 _targetPosition = FindTargetPosition();
            float _maxDistancePlayerAndTargetPosition = 2f;
            if (Vector3.Distance(_targetPosition, transform.position) >= _maxDistancePlayerAndTargetPosition)
            {
                isCentered = false;
            }
        }
        else
        {
            //Find the position and rotation we need to be at
            Vector3 _targetPosition = FindTargetPosition();
            Quaternion _targetRotation = FindTargetRotation();

            //Move just a little bit at the time
            MoveTowards(_targetPosition);
            RotateTowards(_targetRotation);

            //If we have reached the position, don't do any more work
            if (ReachedPosition(_targetPosition))
            {
                isCentered = true;
            }
        }
    }

    private Vector3 FindTargetPosition()
    {
        //Let's get a position in front of the players camera
        Vector3 _tempTargetLocation = cameraTransform.position + (cameraTransform.forward * distance);
        return _tempTargetLocation = new Vector3 (_tempTargetLocation.x, cameraTransform.position.y, _tempTargetLocation.z);
    }

    private Quaternion FindTargetRotation()
    {
        //Let's get a position in front of the players camera
        return cameraTransform.rotation;
    }

    private void MoveTowards(Vector3 _targetPosition)
    {
        //Vector3 _tempTargetPosition = new Vector3(0, transform.position.y, 0);
        transform.position += (_targetPosition - transform.position) * positionSpeedModifier;
    }

    private void RotateTowards(Quaternion _targetRotation)
    {
        Quaternion _tempTargetRotation = Quaternion.Euler(0f, _targetRotation.eulerAngles.y, 0f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _tempTargetRotation, rotationSpeedModifier * Time.deltaTime);
    }

    private bool ReachedPosition(Vector3 _targetPosition)
    {
        //Simple distance check, can be replaced if you want
        return Vector3.Distance(_targetPosition, transform.position) < 0.1f;
    }
}
