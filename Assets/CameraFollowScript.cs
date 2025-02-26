using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0f, 0f, -10f);
    public float zoomLevel = 4.5f; // Lower value = more zoomed in


    public Vector2 minBounds; // Set this in the Inspector or assign dynamically
    public Vector2 maxBounds;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        if (cam != null)
        {
            cam.orthographicSize = zoomLevel;
        }
    }
    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position + offset;

            // Clamp the camera's position to stay within bounds
            float clampedX = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
            float clampedY = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);

            transform.position = new Vector3(clampedX, clampedY, targetPosition.z);
        }
    }
}
