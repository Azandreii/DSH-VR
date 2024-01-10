using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Runtime.InteropServices.WindowsRuntime;

public class ButtonVR : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
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
        ClickEnter,
    }

    //[Header("References")]
    private Image buttonImage;

    private bool hasBeenClicked;
    private bool isCurrentlySelected;
    private bool isHovering;
    private ClickState clickState;

    [Header("Attributes")]
    [SerializeField] private bool rememberSelected;
    private Color buttonColor;
    [SerializeField] bool onlyPlayingState = true;
    [SerializeField] private Color hoverButtonColor = Color.grey;
    [SerializeField] private Color selectButtonColor = Color.black;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        buttonColor = buttonImage.color;
    }

    private void Start()
    {
        InputManagerVR.Instance.OnTrigger += InputManagerVR_OnTrigger;
    }
    private void OnDestroy()
    {
        InputManagerVR.Instance.OnTrigger -= InputManagerVR_OnTrigger;
    }

    private void InputManagerVR_OnTrigger(object sender, InputManagerVR.OnTriggerEventArgs e)
    {
        if (!OnlyPlayingState())
        {
            return; 
        }

        ClearSelected();
        SetImage(buttonColor, buttonColor.a);
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!OnlyPlayingState())
        {
            return;
        }

        clickState = ClickState.ClickDown;
        SetClick(true);
        if (!isCurrentlySelected) { SetImage(selectButtonColor, selectButtonColor.a); }
        if (rememberSelected) { isCurrentlySelected = true; }
        OnClick?.Invoke(this, new OnClickEventArgs { clickState = ClickState.ClickDown });
        if (!isActiveAndEnabled)
        {
            SetClick(false);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!OnlyPlayingState())
        {
            return;
        }

        clickState = ClickState.ClickUp;
        if (!isCurrentlySelected) { SetImage(buttonColor, buttonColor.a); }
        SetClick(false);
        OnClick?.Invoke(this, new OnClickEventArgs { clickState = ClickState.ClickUp });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!OnlyPlayingState())
        {
            return;
        }

        clickState = ClickState.ClickEnter;
        isHovering = true;
        Debug.Log(GetClicked());
        Debug.Log(isCurrentlySelected);

        if (!GetClicked() && !isCurrentlySelected)
        {
            SetImage(hoverButtonColor, hoverButtonColor.a);
        }
        OnClick?.Invoke(this, new OnClickEventArgs { clickState = ClickState.ClickEnter });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!OnlyPlayingState())
        {
            return;
        }

        clickState = ClickState.ClickExit;
        isHovering = false;
        if (GetClicked())
        {
            SetClick(false);
        }
        else if (!isCurrentlySelected)
        {
            SetImage(buttonColor, buttonColor.a);
        }
        OnClick?.Invoke(this, new OnClickEventArgs { clickState = ClickState.ClickExit });
    }

    public void SetSelected(bool _setRememberSelected)
    {
        rememberSelected = _setRememberSelected;
    }

    public void SetClick(bool _bool)
    {
        hasBeenClicked = _bool;
    }

    public bool GetClicked()
    {
        return hasBeenClicked;
    }

    public bool GetHovering()
    {
        return isHovering;
    }

    public bool GetSelected()
    {
        return isCurrentlySelected;
    }

    public Color GetButtonColor()
    {
        return buttonColor;
    }

    public Color GetSelectButtonColor()
    {
        return selectButtonColor;
    }

    public Color GetHoverButtonColor() 
    {
        return hoverButtonColor;
    }

    public void ClearSelected()
    {
        isCurrentlySelected = false;
    }

    public void SetImage(Color _color, float _alpha = 0.1f, bool _onlyAlpha = false)
    {
        switch (_onlyAlpha)
        {
            case true:
                Vector4 imageColor = new Vector4(buttonColor.r, buttonColor.g, buttonColor.b, _alpha);
                buttonImage.color = imageColor;
            break;
            case false:
                imageColor = new Vector4(_color.r, _color.g, _color.b, _alpha);
                buttonImage.color = imageColor;
            break;
        }
    }

    private bool OnlyPlayingState()
    {
        if (onlyPlayingState && GameStateManager.Instance.GetGameStatePlaying())
        {
            return true;
        }
        else if (!onlyPlayingState)
        {
            return true;
        }
        return false;
    }
}
