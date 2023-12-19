using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBarUI : MonoBehaviour
{
    [SerializeField] InternManager internManager;
    [SerializeField] Slider energyBar;

    private void Start()
    {
        internManager.OnEnergyChanged += InternManager_OnEnergyChanged;
    }

    private void InternManager_OnEnergyChanged(object sender, InternManager.OnEnergyChangedEventArgs e)
    {
        energyBar.value = e.energyNormalized;
    }
}
