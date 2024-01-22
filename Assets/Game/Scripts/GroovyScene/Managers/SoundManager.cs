using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("References")]
    [SerializeField] private AudioListSO soundList;

    private GameObject mainCamera;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        mainCamera = InputManagerVR.Instance.GetCamera();
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnTaskCompleted += GameManager_OnTaskCompleted;
            TaskManager.Instance.OnTaskAdded += TaskManager_OnTaskAdded;
            InternSpawnerObject.Instance.OnInternObjectCreated += InternSpawnerObject_OnInternObjectCreated;
            TimeClockManager.Instance.OnDayFinished += TimeClockManager_OnDayFinished;
            ButtonVR.OnClickSound += ButtonVR_OnClickSound;
        }
        SceneLoader.Instance.OnFadeIn += SceneLoader_OnFadeIn;
    }

    private void SceneLoader_OnFadeIn(object sender, System.EventArgs e)
    {
        PlaySound(soundList.fadeSound, mainCamera.transform.position);
    }

    private void ButtonVR_OnClickSound(object sender, System.EventArgs e)
    {
        PlaySound(soundList.buttonPressed, mainCamera.transform.position, 0.3f);
    }

    private void TimeClockManager_OnDayFinished(object sender, System.EventArgs e)
    {
        PlaySound(soundList.gameEnd, mainCamera.transform.position);
    }

    private void InternSpawnerObject_OnInternObjectCreated(object sender, InternSpawnerObject.OnInternObjectCreatedEventArgs e)
    {
        PlaySound(soundList.internEnter, mainCamera.transform.position, 0.8f);
    }

    private void TaskManager_OnTaskAdded(object sender, TaskManager.OnTaskAddedEventArgs e)
    {
        PlaySound(soundList.taskRecieved, mainCamera.transform.position);
    }

    private void GameManager_OnTaskCompleted(object sender, GameManager.OnTaskCompletedEventArgs e)
    {
        PlaySound(soundList.taskCompleted, mainCamera.transform.position);
    }

    public void PlaySound(AudioClip _audioClip, Vector3 _position, float _volume = 1f)
    {
        AudioSource.PlayClipAtPoint(_audioClip, _position, _volume);
    }

    public void PlaySound(AudioSO _audioSO, Vector3 _position, float _volume = 1f)
    {
        AudioSource.PlayClipAtPoint(_audioSO.clip, _position, _volume);
    }
}
