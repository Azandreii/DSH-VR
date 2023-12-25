using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonVR : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public static event EventHandler<OnClickEventArgs> OnClickStatic;
    public event EventHandler<OnClickEventArgs> OnClick;
    public class OnClickEventArgs : EventArgs
    {
        public ClickState clickState;
    }

    public enum ClickState
    {
        ClickDown,
        ClickUp,
        ClickExit,
    }

    [Header("References")]
    [SerializeField] private Image buttonImage;

    private bool hasBeenClicked;

    public void OnPointerDown(PointerEventData eventData)
    {
        SetClick(true);
        OnClick?.Invoke(this, new OnClickEventArgs { clickState = ClickState.ClickDown });
        OnClickStatic?.Invoke(this, new OnClickEventArgs { clickState = ClickState.ClickDown });
        Debug.Log(this.gameObject.name + " Was Clicked");
        Debug.Log(isClicked());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SetClick(false);
        OnClick?.Invoke(this, new OnClickEventArgs { clickState = ClickState.ClickUp });
        OnClickStatic?.Invoke(this, new OnClickEventArgs { clickState = ClickState.ClickUp });
        Debug.Log(this.gameObject.name + " Was released");
        Debug.Log(isClicked());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isClicked())
        {
            SetClick(false);
        }
        OnClick?.Invoke(this, new OnClickEventArgs { clickState = ClickState.ClickExit });
        OnClickStatic?.Invoke(this, new OnClickEventArgs { clickState = ClickState.ClickExit });
        Debug.Log(this.gameObject.name + " Was moved outside of button");
        Debug.Log(isClicked());
    }

    public void SetClick(bool _bool)
    {
        hasBeenClicked = _bool;
    }

    public bool isClicked()
    {
        return hasBeenClicked;
    }

    public void SetImage(Color _color, float _alpha = 10f)
    {
        Vector4 imageColor = new Vector4(_color.r, _color.g, _color.b, _alpha);
        buttonImage.color = imageColor;
    }
}
