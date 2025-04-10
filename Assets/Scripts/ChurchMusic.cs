using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class ChurchMusic : MonoBehaviour
{
    [SerializeField] private EventReference musicEvent;
    private EventInstance musicInstance;

    void Start()
    {
        // Create the FMOD music event instance at start
        musicInstance = RuntimeManager.CreateInstance(musicEvent);
        musicInstance.start();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Church1"))
        {
            musicInstance.setParameterByName("WhichChurch", 0);
        }
        else if (other.CompareTag("Church2"))
        {
            musicInstance.setParameterByName("WhichChurch", 1);
        }
        else if (other.CompareTag("Church3"))
        {
            musicInstance.setParameterByName("WhichChurch", 2);
        }
    }

    void OnDestroy()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();
    }
}