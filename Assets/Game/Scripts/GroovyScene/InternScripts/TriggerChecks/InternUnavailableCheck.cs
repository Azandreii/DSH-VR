using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternUnavailableCheck : MonoBehaviour
{
    private InternManager _internmanager;

    private void Awake()
    {
        _internmanager = GetComponent<InternManager>();
    }



    /* private void EXAMPLE(example example) // // have to detect NPC IS OVERWORKED, then set specific anim state trigger bool to true
     {
         _internmanager.SetIsBoredStatus(true);
     }
    */


    /* private void EXAMPLE(example example) // have to detect UNAVAILABLE TIMEOUT IS OVER, then set specific anim state trigger bool to false
    {
        _internmanager.SetIsBoredStatus(false);
    }
   */

    //NOTE - TUTORIAL WAS INTENDED FOR 2D COLLISION WITH ONTRIGGERENTER2D AND ONTRIGGEREXIT2D, SO IT MIGHT BE DIFFERENT HERE
}
