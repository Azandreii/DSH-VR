using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeSpawner : MonoBehaviour
{
    //Reference to coffee spawn point 
    [SerializeField] private Transform coffeeSpawnPoint;

    //Coffee Object
    [SerializeField]private GameObject coffeeObject;

    //Create coffee
    [SerializeField] private OnCollisionVR boxCollision;

    //Active coffee
    private GameObject activeCoffee;

    private void Start()
    {
        boxCollision.OnCollisionControler += ControllerCollision_OnCollisionControler;
    }

    private void ControllerCollision_OnCollisionControler(object sender, System.EventArgs e)
    {
        if (activeCoffee != null)
        {
            Destroy(activeCoffee);
        }

        GameObject coffeeMug = Instantiate(coffeeObject, coffeeSpawnPoint);
        activeCoffee = coffeeMug;
    }
}
