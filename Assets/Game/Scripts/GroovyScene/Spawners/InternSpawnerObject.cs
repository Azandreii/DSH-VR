using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    [SerializeField] private Transform[] spawnPlaces;
    private List<Transform> possibleSpawnPlaces;
    [SerializeField] private Transform internObjectTemplate;

    [Header("Attributes")]
    [SerializeField] private float internObjectTimerMax = 5f;
    private float timeTillNextInternObject;
    private int internObjectCount;
    private Transform tempRemoveSpawnPlace;

    private void Awake()
    {
        internObjectTemplate.gameObject.SetActive(false);
        Instance = this;
        activeInterns = new List<InternSO>();
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
    }

    private void Update()
    {
        if (internObjectCount < internArraySO.Length && GameStateManager.Instance.GetGameStatePlaying())
        {
            timeTillNextInternObject -= Time.deltaTime;
            if (timeTillNextInternObject < 0)
            {
                timeTillNextInternObject = internObjectTimerMax;
                SpawnIntern();
            }
        }
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
            internObjectCount++;
            AddInternToActiveInternList(_chosenIntern);
            possibleSpawnPlaces.Remove(tempRemoveSpawnPlace);
            tempRemoveSpawnPlace = null;
        }
    }

    public void AddInternToActiveInternList(InternSO _internSO)
    {
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
