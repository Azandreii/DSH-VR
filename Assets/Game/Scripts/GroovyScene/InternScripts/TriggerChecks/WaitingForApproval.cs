using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingForApprovalCheck : MonoBehaviour
{
    [SerializeField] private GameObject LeftHand;
    [SerializeField] private GameObject RightHand;
    [SerializeField] private OnCollisionVR collisionVR;
    [SerializeField] private InternManager internManager;


    private void Start()
    {
        LeftHand = InputManagerVR.Instance.GetLeftControler();
        RightHand = InputManagerVR.Instance.GetRightControler();
        collisionVR.OnCollisionControler += CollisionVR_OnCollisionControler;
    }

    private void CollisionVR_OnCollisionControler(object sender, System.EventArgs e)
    {
        internManager.SetIsWaitingForApprovalStatus(true);
        Debug.Log("set state to WaitingForApproval");
    }

    private void OnTriggerEnter(Collider other) // have to detect the highfive collision, then set specific anim state trigger bool to true
    {
        //Ur an idiot for using this Andrei 
        //Agreed - Andrei
    }

    private void OnTriggerExit(Collider other) // have to detect its been highfived, then set specific anim state trigger bool to false
    {
        if (other.gameObject == LeftHand || other.gameObject == RightHand)
        {
            internManager.SetIsWaitingForApprovalStatus(false);
            Debug.Log("Not WaitingForApproval state anymore");
        }
    } 


    //NOTE - TUTORIAL WAS INTENDED FOR 2D COLLISION WITH ONTRIGGERENTER2D AND ONTRIGGEREXIT2D, SO IT MIGHT BE DIFFERENT HERE
}
