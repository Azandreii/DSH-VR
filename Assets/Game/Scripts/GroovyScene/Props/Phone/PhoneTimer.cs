using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhoneTimer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI hoursText;
    [SerializeField] private TextMeshProUGUI minutesText;
    [SerializeField] private GameObject phoneClockObject;

    private void Start()
    {
        TimeClockManager.Instance.UpdateTimeChanged += TimeClockManager_UpdateTimeChanged;
    }

    private void TimeClockManager_UpdateTimeChanged(object sender, TimeClockManager.OnTimeChangedEventArgs e)
    {
        Debug.Log(e.minutes);
        Debug.Log(e.minutes.ToString());

        ShowTime(e.minutes, e.hours);
    }

    private void ShowTime(float _minuteValue, float _hourValue)
    {
        string _valueToStringMinutes = Mathf.Ceil(_minuteValue).ToString();
        _minuteValue = Mathf.Ceil(_minuteValue);

        if (_minuteValue <= 9)
        {
            _valueToStringMinutes = "0" + _valueToStringMinutes; 
        }
        if (_minuteValue >= 60) 
        {
            _valueToStringMinutes = "00";
            _hourValue++;
        }

        string _valueToStringHours = Mathf.Ceil(_hourValue).ToString();
        if (_hourValue <= 9)
        {
            _valueToStringHours = "0" + _valueToStringHours;
        } 
        if (_hourValue == 24)
        {
            _hourValue = 0;
            _valueToStringHours = "00";
        }

        minutesText.text = _valueToStringMinutes;
        hoursText.text = _valueToStringHours;
    }

    public void Show()
    {
        phoneClockObject.SetActive(true);
    }

    public void Hide()
    {
        phoneClockObject.SetActive(false);
    }
}
