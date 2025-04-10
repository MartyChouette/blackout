using FMOD.Studio;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterHeadBump : MonoBehaviour
{
    
    [Header("Ground Check")]
    [SerializeField] private Transform headCheck;
    [SerializeField] private LayerMask headLayer;

    private bool isHeadBump = false;
    private float lastHeadTime = 0f;
    private RaycastHit2D rh;
    private float distance = 2f;
    private int MaterialValue;
    private LayerMask lm;

    private EventInstance headBumpInstance;

    private void Awake()
    {
       

        if (isHeadBump == null)
        {
            Debug.LogError("GroundCheck is not assigned! Please assign a Transform under the player.");
        }

      
    }

    private void Update()
    {
        isHeadBump = IsHeadBump();

        if (isHeadBump) lastHeadTime = Time.time;
  


    }

    private void FixedUpdate()
    {

    }



    private bool IsHeadBump()
    {
        return Physics2D.OverlapCircle(headCheck.position, 0.3f, headLayer);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "") ;
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


}
