using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class InternSpawner : MonoBehaviour
{
    public static InternSpawner Instance {  get; private set; }
    
    [Header("References")]
    [SerializeField] private InternSO[] internArraySO;
    [SerializeField] private Transform internUI;
    [SerializeField] private Transform internTemplate;

    [Header("Attributes")]
    [SerializeField] private float internTimerMax = 5f;
    private float timeTillNextIntern;
    private int internCount = 0;

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
            CreateNewIntern(internArraySO[internCount]);
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

    public void CreateNewIntern(InternSO _internSO)
    {
        Transform _internUI = Instantiate(internTemplate, internUI);

        //Set the InternManager values
        InternManager im = _internUI.GetComponent<InternManager>();
        im.SetEfficiency(GetEnergyEfficiency(_internSO), GetProcessEfficiency(_internSO));
        im.SetEnergy(GetStartEnergy(_internSO));
        im.SetStateEfficiency(GetAvailableEfficiency(_internSO), GetWorkingEfficiency(_internSO), GetAwaitApprovalEfficiency(_internSO), GetUnavailableEfficiency(_internSO));
        
        //Set the InternObjectUI name
        InternObjectUI ioUI = _internUI.GetComponent<InternObjectUI>();
        ioUI.SetName(GetInternName(_internSO));

        _internUI.gameObject.SetActive(true);
    }

    public Transform GetInternObjectUI(InternSO _internSO)
    {
        return _internSO.internObjectUI;
    }

    public string GetInternName(InternSO _internSO)
    {
        return _internSO.internName;
    }

    public float GetStartEnergy(InternSO _internSO)
    {
        return _internSO.startEnergy;
    }

    public float GetProcessEfficiency(InternSO _internSO)
    {
        return _internSO.processEfficiency;
    }

    public float GetEnergyEfficiency(InternSO _internSO)
    {
        return _internSO.energyEfficiency;
    }

    public float GetAvailableEfficiency(InternSO _internSO)
    {
        return _internSO.availableEfficiency;
    }

    public float GetWorkingEfficiency(InternSO _internSO)
    {
        return _internSO.workingEfficiency;
    }

    public float GetAwaitApprovalEfficiency(InternSO _internSO)
    {
        return _internSO.awaitApprovalEfficiency;
    }

    public float GetUnavailableEfficiency(InternSO _internSO)
    {
        return _internSO.unavailableEfficiency;
    }
}
