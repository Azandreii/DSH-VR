using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeSpawner : MonoBehaviour
{
    //Singelton Reference
    public static CoffeeSpawner Instance;

    //Organizing Unity inspector
    [Header("References")]

    //Reference to coffee spawn point 
    [SerializeField] private Transform coffeeSpawnPoint;

    //Coffee Object
    [SerializeField] private GameObject coffeeObject;

    //Create coffee
    [SerializeField] private OnCollisionVR boxCollision;

    //Materials that will show on the vending machine button
    //when you can and cannot spawn coffee
    [SerializeField] private MeshRenderer buttonMesh;
    [SerializeField] private Material canPressButton;
    [SerializeField] private Material cantPressButton;

    //Active coffee
    private GameObject activeCoffee;

    //Organizing Unity inspector
    [Header("Attributes")]

    //This is the time that locks the player out of spawning coffee when they
    //have spawned coffee, resulting in it not being able to spammed
    [SerializeField] private float coffeeTimerMax = 5f;
    [SerializeField] private float coffeeTimerSpeed = 1f;
    private float coffeeTimer = 0f;
    private bool canSpawnCoffee = false;

    private void Awake()
    {
        //Set Singelton Reference in awake, so you can call the object in start and later
        Instance = this;
    }

    private void Start()
    {
        //C# Event checking the controler collision
        boxCollision.OnCollisionControler += ControllerCollision_OnCollisionControler;
    }
    private void Update()
    {
        //Timer logic for spawning coffee
        if (coffeeTimer > 0 && !canSpawnCoffee)
        { 
            coffeeTimer -= Time.deltaTime * coffeeTimerSpeed;
            if (coffeeTimer <= 0) 
            {
                canSpawnCoffee = true;
                buttonMesh.material = canPressButton;
            }
        }
    }

    private void ControllerCollision_OnCollisionControler(object sender, System.EventArgs e)
    {
        //Collision check logic when pressing the button
        if (coffeeTimer <= 0) 
        { 
            if (activeCoffee != null)
            {
                Destroy(activeCoffee);
            }

            canSpawnCoffee = false;
            buttonMesh.material = cantPressButton;
            coffeeTimer = coffeeTimerMax;
            GameObject coffeeMug = Instantiate(coffeeObject, coffeeSpawnPoint);
            activeCoffee = coffeeMug;
        }
    }

    public void MugUsed()
    {
        //Destroy object when coffee has been assigned to an intern
        Destroy(activeCoffee);
    }
}
