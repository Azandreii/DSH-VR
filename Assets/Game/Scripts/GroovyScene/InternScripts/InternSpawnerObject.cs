using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class InternSpawnerObject : MonoBehaviour
{
    public static InternSpawnerObject Instance;

    public event EventHandler<OnInternObjectCreatedEventArgs> OnInternObjectCreated;
    public class OnInternObjectCreatedEventArgs : EventArgs
    {
        public InternSO internSO;
    }
    
    [Header("References")]
    [SerializeField] private InternSO[] internArraySO;
    private List<InternManager> activeInterns;
    private List<InternSO> possibleInterns;
    [SerializeField] private Transform[] spawnPlaces;
    private List<Transform> possibleSpawnPlaces;

    [Header("Attributes")]
    [SerializeField] private float internObjectTimerMax = 5f;
    private int internObjectCount;
    private int currentSpawnMinute = 1;
    private int previousSpawnMinute = 0;

    private void Awake()
    {
        Instance = this;
        activeInterns = new List<InternManager>();
        possibleInterns = new List<InternSO>();
        foreach (InternSO _internSO in internArraySO) 
        { 
            possibleInterns.Add(_internSO);
        }
        possibleSpawnPlaces = new List<Transform>();
        foreach (Transform _spawnPlace in spawnPlaces)
        {
            possibleSpawnPlaces.Add(_spawnPlace);
        }
    }

    private void Start()
    {
        TimeClockManager.Instance.OnTimeChanged += TimeClockManager_OnTimeChanged;
    }

    private void TimeClockManager_OnTimeChanged(object sender, TimeClockManager.OnTimeChangedEventArgs e)
    {
        int _minutesInt = Mathf.RoundToInt(e.minutes);
        previousSpawnMinute = currentSpawnMinute;
        currentSpawnMinute = _minutesInt;
        for (int i = 0; i < possibleInterns.Count; i++)
        {
            if (e.hours == possibleInterns[i].spawnHour && _minutesInt == possibleInterns[i].spawnMinute)
            {
                SpawnIntern();
            }
            if (e.hours == possibleInterns[i].workTimeHours + possibleInterns[i].spawnHour && _minutesInt == possibleInterns[i].spawnMinute)
            {
                //Clear Spawnpoint occupied and destroy object
                foreach (InternManager _activeInterns in activeInterns)
                {
                    if (_activeInterns.GetInternSO() == possibleInterns[i] && _activeInterns.GetPoint() != null)
                    {
                        Debug.Log("Destroy");
                        _activeInterns.RemovePoint();
                        Destroy(_activeInterns.gameObject);
                    }else
                    {
                        Debug.Log(_activeInterns.GetInternSO());
                    }
                }

                //Remove intern from list, (possibly back in possibleIntern list, but not for now)
                possibleInterns.Remove(possibleInterns[i]);

                //Decrease internObjectCount
                //internObjectCount--;
            }
        }
    }

    private void SpawnIntern()
    {
        if (internObjectCount < internArraySO.Length)
        {
            InternSO _chosenIntern = internArraySO[internObjectCount];
            Transform _spawnPoint = RandomSpawnPlace();
            Transform _internObject = Instantiate(_chosenIntern.internObjectVR.transform, _spawnPoint);

            //Attach the scriptableObject to the InternManager
            InternManager im = _internObject.GetComponent<InternManager>();
            im.SetInternSO(_chosenIntern);
            im.SetPoint(_spawnPoint.GetComponent<SpawnSpot>());
            AddInternToActiveInternList(_chosenIntern, im);
            Debug.Log("General Intern Spawn");
        }
    }

    public void AddInternToActiveInternList(InternSO _internSO, InternManager _internObject)
    {
        internObjectCount++;
        activeInterns.Add(_internObject);
        OnInternObjectCreated?.Invoke(this, new OnInternObjectCreatedEventArgs
        {
            internSO = _internSO,
        });
    }

    private Transform RandomSpawnPlace()
    {
        //Check for occupied instead of removing from list
        Transform _tempRemoveSpawnPlace = possibleSpawnPlaces[UnityEngine.Random.Range(0, possibleSpawnPlaces.Count)];
        _tempRemoveSpawnPlace.GetComponent<SpawnSpot>().SetOccupied(true);
        possibleSpawnPlaces.Remove(_tempRemoveSpawnPlace);
        return _tempRemoveSpawnPlace;
    }
}
