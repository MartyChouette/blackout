using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class FallSoundController : MonoBehaviour
{
   
    public string fallSoundEvent;  // Set this to your FMOD event, e.g., "event:/Character/Fall"

    private Rigidbody2D rb;
    private bool isFalling = false;

    // Adjust this threshold to determine when the character is considered to be falling
    public float fallVelocityThreshold = -2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if the character is falling (i.e., vertical speed exceeds threshold)
        if (rb.linearVelocity.y < fallVelocityThreshold)
        {
            isFalling = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Assuming the ground is tagged as "Ground"
        if (isFalling && collision.gameObject.CompareTag("Ground"))
        {
            // Play the fall sound at the character's position
            RuntimeManager.PlayOneShot(fallSoundEvent, transform.position);

            // Reset the falling flag so the sound only plays once per fall
            isFalling = false;
        }
    }
}
