using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform target;  // Player transform
    [SerializeField] private float followSpeed = 5f;  // Base follow speed

    [Header("Look Ahead Settings")]
    [SerializeField] private float lookAheadDistance = 2f;  // How far ahead the camera looks
    [SerializeField] private float lookAheadSmoothing = 0.1f;  // Smoothing for look ahead

    [Header("Dead Zone Settings")]
    [SerializeField] private Vector2 deadZoneSize = new Vector2(1f, 0.5f);  // Dead zone area where the camera doesn't move

    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPosition;
    private float lookAheadX = 0f;
    private float lastTargetX;

    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("CameraFollow: No target assigned! Assign the player in the inspector.");
            return;
        }

        lastTargetX = target.position.x;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 playerPos = target.position;
        Vector3 cameraPos = transform.position;

        // Look ahead effect (only applies when moving horizontally)
        float moveDeltaX = playerPos.x - lastTargetX;
        if (Mathf.Abs(moveDeltaX) > 0.01f)
        {
            lookAheadX = Mathf.Lerp(lookAheadX, Mathf.Sign(moveDeltaX) * lookAheadDistance, lookAheadSmoothing);
        }

        lastTargetX = playerPos.x;

        // Dead zone logic
        float deadZoneLeft = cameraPos.x - deadZoneSize.x;
        float deadZoneRight = cameraPos.x + deadZoneSize.x;
        float deadZoneTop = cameraPos.y + deadZoneSize.y;
        float deadZoneBottom = cameraPos.y - deadZoneSize.y;

        float targetX = (playerPos.x < deadZoneLeft || playerPos.x > deadZoneRight) ? playerPos.x + lookAheadX : cameraPos.x;
        float targetY = (playerPos.y < deadZoneBottom || playerPos.y > deadZoneTop) ? playerPos.y : cameraPos.y;

        targetPosition = new Vector3(targetX, targetY, cameraPos.z);

        // Smooth damp to the target position
        transform.position = Vector3.SmoothDamp(cameraPos, targetPosition, ref velocity, 1 / followSpeed);
    }
}
