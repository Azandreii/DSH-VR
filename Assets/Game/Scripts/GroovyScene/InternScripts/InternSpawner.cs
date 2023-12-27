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
    
    [Header("References")]
    [SerializeField] private InternSO[] internArraySO;
    [SerializeField] private Transform internUI;
    [SerializeField] private Transform internTemplate;

    [Header("Attributes")]
    [SerializeField] private float internTimerMax = 5f;
    private float timeTillNextIntern;
    private int internCount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        internTemplate.gameObject.SetActive(false);
        timeTillNextIntern = internTimerMax;
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
            InternManager im = _internUI.GetComponent<InternManager>();
            im.SetInternSO(internArraySO[internCount]);
            internCount++;
        }
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
