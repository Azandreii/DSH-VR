using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resource
{
    int[] resources = new int[4];

    public int AdjustResource(ResourceType type, int number)
    {
        if (resources[(int)type] >= number)
        {
            resources[(int)type] -= number;
            return number;
        }
        else if (resources[(int)type] < number && resources[(int)type] > 0)
        {
            int resource = resources[(int)type];
            resources[(int)type] = 0;
            return resource;
        }
        return 0;
    }

    public enum ResourceType
    {
        Food,
        Wood,
        Gold,
        Stone,
    }
}
