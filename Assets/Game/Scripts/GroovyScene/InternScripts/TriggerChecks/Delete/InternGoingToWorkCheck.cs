using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternGoingToWorkCheck : MonoBehaviour
{
    private InternManager _internmanager;

    private void Awake()
    {
        _internmanager = GetComponent<InternManager>();
    }



    /* private void EXAMPLE(example example) // // have to detect its been given work, then set specific anim state trigger bool to true
     {
         internManager.SetIsBoredStatus(true);
     }
    */


    /* private void EXAMPLE(example example) // have to detect its working, then set specific anim state trigger bool to false
    {
        internManager.SetIsBoredStatus(false);
    }
   */

    //NOTE - TUTORIAL WAS INTENDED FOR 2D COLLISION WITH ONTRIGGERENTER2D AND ONTRIGGEREXIT2D, SO IT MIGHT BE DIFFERENT HERE
}
