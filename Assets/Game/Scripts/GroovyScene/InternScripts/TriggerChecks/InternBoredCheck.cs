using Sirenix.OdinInspector.Editor.Examples.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternBoredCheck : MonoBehaviour
{
    private InternManager _internmanager;

    private void Awake()
    {
        _internmanager = GetComponent<InternManager>();
    }



    /* private void EXAMPLE(example example) // // have to detect the 30s without work, then set specific anim state trigger bool to true
     {
         internManager.SetIsBoredStatus(true);
     }
    */


    /* private void EXAMPLE(example example) // have to detect its been given a task, then set specific anim state trigger bool to false
    {
        internManager.SetIsBoredStatus(false);
    }
   */  

    //NOTE - TUTORIAL WAS INTENDED FOR 2D COLLISION WITH ONTRIGGERENTER2D AND ONTRIGGEREXIT2D, SO IT MIGHT BE DIFFERENT HERE
}
