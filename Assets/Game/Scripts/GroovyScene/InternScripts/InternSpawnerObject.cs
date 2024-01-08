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
    private List<InternSO> activeInterns;
    private List<InternSO> possibleInterns;
    [SerializeField] private Transform[] spawnPlaces;
    private List<Transform> possibleSpawnPlaces;
    [SerializeField] private Transform internObjectTemplate;

    [Header("Attributes")]
    [SerializeField] private float internObjectTimerMax = 5f;
    private float timeTillNextInternObject;
    private int internObjectCount;
    private Transform tempRemoveSpawnPlace;
    private int currentSpawnMinute;
    private int previousSpawnMinute;

    private void Awake()
    {
        internObjectTemplate.gameObject.SetActive(false);
        Instance = this;
        activeInterns = new List<InternSO>();
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
        timeTillNextInternObject = internObjectTimerMax;
    }

    private void Start()
    {
        internObjectTemplate.gameObject.SetActive(false);
        TimeClockManager.Instance.OnTimeChanged += TimeClockManager_OnTimeChanged;
    }

    private void TimeClockManager_OnTimeChanged(object sender, TimeClockManager.OnTimeChangedEventArgs e)
    {
        /*bool _newMinute = false;
        currentSpawnMinute = Mathf.RoundToInt(e.minutes);
        foreach (InternSO _internSO in possibleInterns)
        {
            if (e.hours == _internSO.spawnHour && Mathf.RoundToInt(e.minutes) == _internSO.spawnMinute && _newMinute)
            {
                Debug.Log("Spawn");
                SpawnIntern();
            }
        }
        if (previousSpawnMinute != currentSpawnMinute)
        {
            _newMinute = true;
        }
        previousSpawnMinute = Mathf.RoundToInt(e.minutes);*/

        /*if (internObjectCount < internArraySO.Length && GameStateManager.Instance.GetGameStatePlaying())
        {
            timeTillNextInternObject -= Time.deltaTime;
            if (timeTillNextInternObject < 0)
            {
                timeTillNextInternObject = internObjectTimerMax;
                
            }
        }*/
    }

    private void SpawnIntern()
    {
        if (internObjectCount < internArraySO.Length)
        {
            InternSO _chosenIntern = internArraySO[internObjectCount];
            Transform _internObject = Instantiate(_chosenIntern.internObjectVR.transform, RandomSpawnPlace());

            //Attach the scriptableObject to the InternManager
            InternManager im = _internObject.GetComponent<InternManager>();
            im.SetInternSO(_chosenIntern);
            AddInternToActiveInternList(_chosenIntern);
            possibleSpawnPlaces.Remove(tempRemoveSpawnPlace);
            tempRemoveSpawnPlace = null;
        }
    }

    public void AddInternToActiveInternList(InternSO _internSO)
    {
        internObjectCount++;
        activeInterns.Add(_internSO);
        OnInternObjectCreated?.Invoke(this, new OnInternObjectCreatedEventArgs
        {
            internSO = _internSO,
        });
    }

    private Transform RandomSpawnPlace()
    {
        tempRemoveSpawnPlace = possibleSpawnPlaces[UnityEngine.Random.Range(0, possibleSpawnPlaces.Count)];
        tempRemoveSpawnPlace.GetComponent<SpawnSpot>().SetOccupied(true);
        return tempRemoveSpawnPlace;
    }

    public void AdjustInternCount(int _amount)
    {
        internObjectCount += _amount;
    }
}
