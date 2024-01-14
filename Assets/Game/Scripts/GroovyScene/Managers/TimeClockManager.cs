using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class TimeClockManager : MonoBehaviour
{
    public static TimeClockManager Instance;
    public event EventHandler<OnTimeChangedEventArgs> UpdateTimeChanged;

    public event EventHandler<OnTimeChangedEventArgs> OnTimeChanged;
    public class OnTimeChangedEventArgs : EventArgs
    {
        public float minutes;
        public float hours;
    }
    public event EventHandler OnDayFinished;

    [Header("References")]
    [SerializeField] private Transform clockArmMinutes;
    [SerializeField] private Transform clockArmHours;

    [Header("Attributes")]
    [PropertyRange(0, 23), InfoBox("Make sure Timer Max is higher than Hours Start!")]
    [SerializeField] private int timerMax;
    [PropertyRange(0, 59), HideInPlayMode]
    [SerializeField] private int minutesStart;
    [PropertyRange(0, 23), HideInPlayMode]
    [SerializeField] private int hoursStart;
    [SerializeField] private float timerSpeed = 3f;
    private float currentMinutes;
    private float currentHours;
    private float previousMinute;

    private void Awake()
    {
        Instance = this;
        currentMinutes = minutesStart;
        currentHours = hoursStart;
        minutesStart = Mathf.RoundToInt(MinutesZeroReference(minutesStart));
        clockArmMinutes.eulerAngles = SetTimeMinutes(minutesStart);
        hoursStart = Mathf.RoundToInt(HourZeroReference(hoursStart));
        clockArmHours.eulerAngles = SetTimeHours(hoursStart);
    }

    private void Update()
    {
        if (GameStateManager.Instance.IsGamePlaying()) {
            clockArmMinutes.eulerAngles = AdjustTimeMinutes(timerSpeed);
            clockArmHours.eulerAngles = AdjustTimeHours(timerSpeed);
            
            previousMinute = Mathf.RoundToInt(currentMinutes);
            int _minutesMultiplier = 6;
            currentMinutes = currentMinutes + ((Time.deltaTime * timerSpeed) / _minutesMultiplier);
            if (currentMinutes >= 60)
            {
                //In game, an hour has passed
                currentMinutes = 0;
                currentHours++;
                if (currentHours == timerMax)
                {
                    //In game, the timer maximum has been reached

                    //Set in game state manager
                    GameStateManager.Instance.SetGamestate(GameStateManager.GameState.GameOver);

                    OnDayFinished?.Invoke(this, EventArgs.Empty);
                }
            }
            if (MinuteChanged(currentMinutes))
            {
                OnTimeChanged?.Invoke(this, new OnTimeChangedEventArgs
                {
                    minutes = currentMinutes,
                    hours = currentHours,
                });
            }
            UpdateTimeChanged?.Invoke(this, new OnTimeChangedEventArgs
            {
                minutes = currentMinutes,
                hours = currentHours,
            });

        }
    }

    private bool MinuteChanged(float _newValue)
    {
        int _intCurrentMinutes = Mathf.RoundToInt(_newValue);
        if (previousMinute != _intCurrentMinutes)
        {
            previousMinute = _intCurrentMinutes;
            return true;
        }
        return false;
    }

    private Vector3 AdjustTimeMinutes(float _timerSpeed)
    {
        float _minuteArmRotationSpeed = _timerSpeed * Time.deltaTime;
        Vector3 _minuteArmRotation = new Vector3(clockArmMinutes.eulerAngles.x, clockArmMinutes.eulerAngles.y,
            clockArmMinutes.eulerAngles.z + _minuteArmRotationSpeed);
        return _minuteArmRotation;
    }

    private Vector3 AdjustTimeHours(float _timerSpeed)
    {
        float _setHours = 12f;
        float _hourArmRotationSpeed = (_timerSpeed * Time.deltaTime) / _setHours;
        Vector3 _hourArmRotation = new Vector3(clockArmHours.eulerAngles.x, clockArmHours.eulerAngles.y,
            clockArmHours.eulerAngles.z + _hourArmRotationSpeed);
        return _hourArmRotation;
    }

    private Vector3 SetTimeMinutes(float _setMinuteTime)
    {
        Vector3 _longArmRotationHandler = new Vector3(clockArmMinutes.eulerAngles.x, clockArmMinutes.eulerAngles.y,
            _setMinuteTime);
        return _longArmRotationHandler;
    }

    private Vector3 SetTimeHours(float _setHourTime)
    {
        Vector3 _hourArmRotation = new Vector3(clockArmHours.eulerAngles.x, clockArmHours.eulerAngles.y,
            clockArmHours.eulerAngles.z + _setHourTime);
        return _hourArmRotation;
    }

    private float MinutesZeroReference(float _minutes)
    {
        int _setMinutesToZero = 1;
        int _minutesMultiplier = 6;
        float _minutesNormalized = (_minutes * _minutesMultiplier) + (_setMinutesToZero * _minutesMultiplier);
        return _minutesNormalized;
    }

    private float HourZeroReference(float _hours)
    {
        int _setHoursToZero = 6;
        int _hoursMultiplier = 30;
        float _hoursNormalized = (_hours * _hoursMultiplier) - (_setHoursToZero * _hoursMultiplier);
        return _hoursNormalized;
    }
}
