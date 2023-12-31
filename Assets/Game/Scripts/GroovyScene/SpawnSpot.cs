using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpot : MonoBehaviour
{
    private bool isOccupied;

    public bool GetOccupied()
    {
        return isOccupied;
    }

    public void SetOccupied(bool _isOccupied)
    {
        isOccupied = _isOccupied;
    }
}
