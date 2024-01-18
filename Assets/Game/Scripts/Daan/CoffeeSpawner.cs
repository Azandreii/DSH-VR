using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeSpawner : MonoBehaviour
{
    //Reference to coffee spawn point 
    [SerializeField] private Transform coffeeSpawnPoint;

    //Coffee Object
    [SerializeField]private GameObject coffeeObject;

    [SerializeField] private OnCollisionVR boxCollision;

    private void Start()
    {
        boxCollision.OnCollisionControler += ControllerCollision_OnCollisionControler;
    }

    private void ControllerCollision_OnCollisionControler(object sender, System.EventArgs e)
    {
        Instantiate(coffeeObject, coffeeSpawnPoint);
    }
}
