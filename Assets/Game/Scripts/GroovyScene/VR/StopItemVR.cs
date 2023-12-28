using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopItemVR : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject internObjectUI;
    private bool isStashed;

    public void EnterSocket()
    {
        isStashed = true;
    }

    public void ExitSocket()
    {
        isStashed = false;
    }

    public bool IsStashed()
    {
        return isStashed;
    }
}
