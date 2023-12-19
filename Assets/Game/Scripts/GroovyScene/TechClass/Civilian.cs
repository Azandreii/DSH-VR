using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Resource;

[System.Serializable]
public class Civilian : Components.Component
{
    [SerializeField]
    int[] maximum = new int[4];
    int[] current = new int[4];

    public void AdjustResource(Resource resource, ResourceType type, int number)
    {
        // It is possible to add resources
        // that are not carried (should be returned)
        int gathered = resource.AdjustResource(type, number);

        current[(int)type] = gathered;

        if (current[(int)type] > maximum[(int)type])
        {
            int difference = current[(int)type] - maximum[(int)type];
            current[(int)type] = maximum[(int)type];
            resource.AdjustResource(type, difference);
        }
    }
}
