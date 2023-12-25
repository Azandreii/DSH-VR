using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonVR : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image buttonImage;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name + " Was Clicked");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name + " Was Clicked");
    }
}
