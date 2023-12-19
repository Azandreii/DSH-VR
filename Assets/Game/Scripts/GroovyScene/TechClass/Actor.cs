using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Components;

public class Actor : MonoBehaviour
{
    Composite composite = new Composite();

    StateMachine<Actor> stateMachine;

    Actor target;

    [SerializeField] UnitType type;
    public Actor Target { get => target; }

    private void Start()
    {
        switch (type)
        {
            case UnitType.Civilian:
                composite.AddComponent(new Civilian());
                break;

            case UnitType.Soldier:
                break;
        }
    }

    
    // Start is called before the first frame update
    void Awake()
    {
        stateMachine = new StateMachine<Actor>(this);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Execute();
    }

    public enum UnitType
    {
        Soldier,
        Civilian,
    }
}
