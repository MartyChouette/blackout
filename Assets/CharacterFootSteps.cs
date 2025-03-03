using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEditor;

public class CharacterFootSteps : MonoBehaviour
{

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


    private int MaterialValue;
    private RaycastHit2D rh;
    private float distance = 2f;
    private string EventPath = "event:/footsteps";
    private PARAMETER_ID TerrainParamID;
    private PARAMETER_ID WalkRunParamID;
    private LayerMask lm;

    private Rigidbody2D rb;
    private bool isMovingForward;
    private bool isPlayingSound;
    private float footstepCooldown = 1f; // Adjust based on walking speed
    private float nextStepTime = 0f;

    private EventInstance footstepInstance;

    public bool isGrounded;


    private void Start()
    {
        
        
        rb = GetComponent<Rigidbody2D>();

        // Create the footstep event instance
        footstepInstance = RuntimeManager.CreateInstance(EventPath);

        // Get FMOD parameter IDs dynamically
        EventDescription eventDescription;
        RuntimeManager.StudioSystem.getEvent(EventPath, out eventDescription);

        PARAMETER_DESCRIPTION paramDesc;
        eventDescription.getParameterDescriptionByName("terrain", out paramDesc);
        TerrainParamID = paramDesc.id;

        eventDescription.getParameterDescriptionByName("walk_run", out paramDesc);
        WalkRunParamID = paramDesc.id;

        lm = LayerMask.GetMask("Ground");
    }

    private void Update()
    {

        isGrounded = IsGrounded();

        isMovingForward = Mathf.Abs(rb.linearVelocity.x) > 0.01f;
        
        
        if (isMovingForward && Time.time >= nextStepTime && isGrounded)
        {
            PlayFootstep();
            nextStepTime = Time.time + footstepCooldown;
        }
        else if ((!isMovingForward && isPlayingSound) || !isGrounded && isPlayingSound)
        {
            StopFootstep();
        }

        Debug.DrawRay(transform.position, Vector2.down * distance, Color.blue);
    }

    private void PlayFootstep()
    {
        isPlayingSound = true;
        MaterialCheck(); // Update terrain type

        // Set terrain parameter dynamically
        footstepInstance.setParameterByID(TerrainParamID, MaterialValue);
        footstepInstance.setParameterByID(WalkRunParamID, 1);

        footstepInstance.start();
    }

    private void StopFootstep()
    {
        isPlayingSound = false;
        footstepInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    private void MaterialCheck()
    {
        rh = Physics2D.Raycast(transform.position, Vector2.down, distance, lm);
        if (rh.collider != null)
        {
            switch (rh.collider.tag)
            {
                case "dirty_ground": MaterialValue = 0; break;
                case "grass": MaterialValue = 1; break;
                case "gravel": MaterialValue = 2; break;
                case "leaves": MaterialValue = 3; break;
                case "metal": MaterialValue = 4; break;
                case "mud": MaterialValue = 5; break;
                case "rock": MaterialValue = 6; break;
                case "sand": MaterialValue = 7; break;
                case "snow": MaterialValue = 8; break;
                case "tile": MaterialValue = 9; break;
                case "water": MaterialValue = 10; break;
                case "wood": MaterialValue = 11; break;
                default: MaterialValue = 12; break; // Default sound
            }
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
    }
}