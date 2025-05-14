using UnityEngine;
using FMODUnity;
public class AudioPreLoad : MonoBehaviour
{
    private void Awake()
    {
        RuntimeManager.LoadBank("bump", true);
        RuntimeManager.LoadBank("chime", true);
        RuntimeManager.LoadBank("ELDER1", true);
        RuntimeManager.LoadBank("ELDER2", true);
        RuntimeManager.LoadBank("ELDER3", true);
        RuntimeManager.LoadBank("Fall", true);
        RuntimeManager.LoadBank("Final", true);
        RuntimeManager.LoadBank("headbump", true);
        RuntimeManager.LoadBank("Jump", true);
        RuntimeManager.LoadBank("Land", true);
        RuntimeManager.LoadBank("Master", true);
        RuntimeManager.LoadBank("Music", true);
        RuntimeManager.LoadBank("Music2", true);
        RuntimeManager.LoadBank("Music3", true);
        RuntimeManager.LoadBank("Secret_Passage", true);
        RuntimeManager.LoadBank("SFX", true);
        RuntimeManager.LoadBank("SpokenEnd", true);
        RuntimeManager.LoadBank("welcome1", true);
        RuntimeManager.LoadBank("welcome2", true);
        RuntimeManager.LoadBank("welcome3", true);
        RuntimeManager.LoadBank("Wind_1", true);
        RuntimeManager.LoadBank("Wind_2", true);
        RuntimeManager.LoadBank("Wind_3", true);

    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
