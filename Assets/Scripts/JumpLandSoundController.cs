using UnityEngine;
using FMODUnity;

public class JumpLandSoundController : MonoBehaviour
{
    public string jumpSoundEvent;  // e.g., "event:/Character/Jump"

    public string landSoundEvent;  // e.g., "event:/Character/Land"

    private Rigidbody2D rb;
    private bool isInAir = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isInAir)
        {
            Debug.Log("Jump initiated");
            RuntimeManager.PlayOneShot(jumpSoundEvent, transform.position);
            isInAir = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Landed on ground");
            if (isInAir)
            {
                RuntimeManager.PlayOneShot(landSoundEvent, transform.position);
                isInAir = false;
            }
        }
    }

}