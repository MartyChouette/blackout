using UnityEngine;

public class ToggleCanvas : MonoBehaviour
{
    // Assign your Canvas GameObject from the Inspector.
    // Make sure this Canvas is not the same GameObject as the one this script is attached to.
    public GameObject targetCanvas;

    void Update()
    {
        // Listen for the 'O' key press.
        if (Input.GetKeyDown(KeyCode.O))
        {
            // Ensure targetCanvas is assigned.
            if (targetCanvas != null)
            {
                // Toggle the active state:
                // If active, deactivate; if inactive, activate.
                targetCanvas.SetActive(!targetCanvas.activeSelf);
            }
            else
            {
                Debug.LogWarning("targetCanvas is not assigned in the Inspector.");
            }
        }
    }
}