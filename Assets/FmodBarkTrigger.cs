using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System;

[RequireComponent(typeof(Collider2D))]
public class FmodBarkTrigger : MonoBehaviour
{
    public EventReference fmodEvent; // Replaces [EventRef] string
    public bool playOnce = false;
    public float cooldownTime = 5f;

    private EventInstance barkInstance;
    private float lastTriggeredTime = -Mathf.NegativeInfinity;
    private bool hasPlayed = false;
    private bool isPlaying = false;

    private static EVENT_CALLBACK stopCallback;

    private void Awake()
    {
        stopCallback = new EVENT_CALLBACK(OnBarkStopped);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (playOnce && hasPlayed)
            return;

        float timeSinceLastTrigger = Time.time - lastTriggeredTime;

        if (timeSinceLastTrigger < cooldownTime && isPlaying)
            return; 

        // If enough time has passed, interrupt current and play new
        if (barkInstance.isValid())
        {
            barkInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            barkInstance.release();
        }

        PlayBark();
    }

    private void PlayBark()
    {
        barkInstance = RuntimeManager.CreateInstance(fmodEvent);
        RuntimeManager.AttachInstanceToGameObject(barkInstance, transform, GetComponent<Rigidbody2D>());
        barkInstance.setCallback(stopCallback, EVENT_CALLBACK_TYPE.STOPPED);
        barkInstance.start();
        barkInstance.release();

        isPlaying = true;
        lastTriggeredTime = Time.time;

        if (playOnce)
            hasPlayed = true;
    }

    private void Update()
    {
        if (barkInstance.isValid())
        {
            barkInstance.getPlaybackState(out PLAYBACK_STATE state);
            isPlaying = (state == PLAYBACK_STATE.PLAYING);
        }
        else
        {
            isPlaying = false;
        }
    }

    private static FMOD.RESULT OnBarkStopped(EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameters)
    {
        return FMOD.RESULT.OK; // Placeholder, not using for now
    }

    public void ResetTrigger()
    {
        hasPlayed = false;
        lastTriggeredTime = -Mathf.NegativeInfinity;
    }

    public void ForceStop()
    {
        if (barkInstance.isValid())
        {
            barkInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            barkInstance.release();
        }

        isPlaying = false;
    }
}
