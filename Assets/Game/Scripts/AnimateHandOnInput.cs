using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;

public class AnimateHandOnInput : MonoBehaviour
{
    //I always write headers to seperate my code between References and Attributes. Here the references are specified because it is clearer towards other people.
    //References are always to other objects (often in the scene), while attributes are number that you can tweek on the object itself (like movementSpeed)
    [Title("VR Controller Input References"), DisableInPlayMode, InfoBox("These values work output a float, which is used for the hand animation. Changing these references might affect the animation.")]
    public InputActionProperty pinchAnimationAction;
    [DisableInPlayMode]
    public InputActionProperty gripAnimationAction;
    [Title("General References"), DisableInPlayMode]
    public Animator handAnimator;

    //Static strings decrease the chance of errors when working with strings (like this SetFloat) *Groovy
    static private string TRIGGER = "Trigger";
    static private string GRIP = "Grip";

    private void Update()
    {
        float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat(TRIGGER, triggerValue);

        float gripValue = gripAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat(GRIP, gripValue);
    }
}
