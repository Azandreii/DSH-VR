using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class InternSpawner : MonoBehaviour
{
    public static InternSpawner Instance;

    public event EventHandler<OnInternCreatedEventArgs> OnInternCreated;
    public class OnInternCreatedEventArgs : EventArgs
    {
        public InternSO internSO;
    }
    
    [Header("References")]
    [SerializeField] private InternSO[] internArraySO;
    private List<InternSO> activeInterns;
    [SerializeField] private Transform internUI;
    [SerializeField] private Transform internTemplate;

    [Header("Attributes")]
    [SerializeField] private float internTimerMax = 5f;
    private float timeTillNextIntern;
    private int internCount;

    private void Awake()
    {
        Instance = this;
        activeInterns = new List<InternSO>();
        timeTillNextIntern = internTimerMax;
    }

    private void Start()
    {
        internTemplate.gameObject.SetActive(false);
        UpdateVisual();
    }

    private void Update()
    {
        if (internCount < internArraySO.Length)
        {
            timeTillNextIntern -= Time.deltaTime;
            if (timeTillNextIntern < 0)
            {
                timeTillNextIntern = internTimerMax;
                SpawnIntern();
            }
        }
    }

    private void SpawnIntern()
    {
        if (internCount < internArraySO.Length)
        {
            Transform _internUI = Instantiate(internTemplate, internUI);

            //Attach the scriptableObject to the InternManager
            InternSO chosenIntern = internArraySO[internCount];
            InternManager im = _internUI.GetComponent<InternManager>();
            im.SetInternSO(chosenIntern);
            internCount++;
            AddInternToActiveInternList(chosenIntern);
        }
    }

    public void AddInternToActiveInternList(InternSO _internSO)
    {
        Debug.Log(_internSO);
        activeInterns.Add(_internSO);
        OnInternCreated?.Invoke(this, new OnInternCreatedEventArgs
        {
            internSO = _internSO,
        });
    }

    private void UpdateVisual()
    {
        foreach (Transform child in internUI)
        {
            if (child == internTemplate) continue;
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void AdjustInternCount(int _amount)
    {
        internCount += _amount;
    }
}
