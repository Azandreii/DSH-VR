using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : MonoBehaviour
{
    [SerializeField] private TaskSO coffeeTask;

    public void SetCoffeeTask()
    {
        GameManager.Instance.SetTaskSO(coffeeTask, gameObject);
    }
    public void RemoveCoffeeTask()
    {
        GameManager.Instance.SetTaskSO(null, null);
    }

}
