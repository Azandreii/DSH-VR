using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternHighfiveableCheck : MonoBehaviour
{
    private InternManager _internmanager;

    private void Awake()
    {
        _internmanager = GetComponent<InternManager>();
    }



    /* private void EXAMPLE(example example) // // have to detect it finished work, then set specific anim state trigger bool to true
     {
         _internmanager.SetIsBoredStatus(true);
     }
    */


    /* private void EXAMPLE(example example) // have to detect its been highfived, then set specific anim state trigger bool to false
    {
        _internmanager.SetIsBoredStatus(false);
    }
   */

    //NOTE - TUTORIAL WAS INTENDED FOR 2D COLLISION WITH ONTRIGGERENTER2D AND ONTRIGGEREXIT2D, SO IT MIGHT BE DIFFERENT HERE
}
