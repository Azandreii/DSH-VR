using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlushSound : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioClip boopClip;
    [SerializeField] private Transform soundPoint;
    [SerializeField] private OnCollisionVR noseCollision;

    [Header("Attributes")]
    [SerializeField] private float volume = 1f;
    private bool isBooped = false;
    private float timeTillNextBoop = 0f;
    private float boopTimeMax = 1.1f;

    private void Start()
    {
        noseCollision.OnCollisionControler += NoseCollision_OnCollisionControler;
    }

    private void Update()
    {
        if (isBooped)
        {
            timeTillNextBoop -= Time.deltaTime;
            if (timeTillNextBoop <= 0)
            {
                isBooped = false;
            }
        }
    }

    private void NoseCollision_OnCollisionControler(object sender, System.EventArgs e)
    {
        if (isBooped == false)
        {
            SoundManager.Instance.PlaySound(boopClip, soundPoint.position, volume);
            timeTillNextBoop = boopTimeMax;
            isBooped = true;
        }
    }
}
