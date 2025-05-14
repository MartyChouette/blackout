using UnityEngine;
using TMPro;

public class FlashTMPText : MonoBehaviour
{
    public TextMeshProUGUI tmpText;
    public float flashSpeed = 1f; // Speed of the flash (lower = slower)

    private void Update()
    {
        if (tmpText != null)
        {
            float alpha = (Mathf.Sin(Time.time * flashSpeed) + 1f) / 2f; // Range 0 to 1
            Color color = tmpText.color;
            color.a = alpha;
            tmpText.color = color;
        }
    }
}