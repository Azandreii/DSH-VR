using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManagerVR : MonoBehaviour
{
    public static ClickManagerVR Instance;

    private GameObject selectedGameObject;

    private void Awake()
    {
        Instance = this;
    }

    public void SetGameObject(GameObject _gameObject)
    {
        selectedGameObject = _gameObject;
    }

    public GameObject GetGameObject()
    {
        return selectedGameObject;
    }
}
