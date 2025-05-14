using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;

[RequireComponent(typeof(Collider2D))]
public class SceneReloader2DTrigger : MonoBehaviour
{
    public Image fadePanel;
    public float fadeDuration = 1f;
    private bool isRestarting = false;
    private float fadeTimer = 0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isRestarting && other.CompareTag("Player"))
        {
            StartFade();
        }
    }

    private void Update()
    {
        if (isRestarting)
        {
            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Clamp01(fadeTimer / fadeDuration);

            if (fadePanel != null)
            {
                Color color = fadePanel.color;
                color.a = alpha;
                fadePanel.color = color;
            }

            if (fadeTimer >= fadeDuration)
            {
                ReloadCurrentScene();
            }
        }
    }

    void StartFade()
    {
        isRestarting = true;
        fadeTimer = 0f;

        if (fadePanel != null)
        {
            fadePanel.gameObject.SetActive(true);
        }
    }

    void ReloadCurrentScene()
    {
        StopAllFMODEvents();

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    void StopAllFMODEvents()
    {
        RuntimeManager.StudioSystem.flushCommands();
        Bus masterBus;
        RuntimeManager.StudioSystem.getBus("bus:/", out masterBus);
        masterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

}