using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    public float moveSpeed = 2f; 
    public float changeDirectionInterval = 2f; 
    public ZoneType zoneType; 

    private Vector2 targetPosition;
    private float timer;

    void Start()
    {
        SetNewTargetPosition();
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetNewTargetPosition();
        }

        // Change direction periodically
        timer += Time.deltaTime;
        if (timer >= changeDirectionInterval)
        {
            SetNewTargetPosition();
            timer = 0f;
        }
    }

    void SetNewTargetPosition()
    {
        float zoneWidth = 25f;
        float zoneHeight = 10f;

        float minX, maxX, minY, maxY;

        switch (zoneType)
        {
            case ZoneType.Abyss:
                minX = 0f;
                maxX = zoneWidth / 3f;
                minY = 0f;
                maxY = zoneHeight / 3f;
                break;

            case ZoneType.Middle:
                minX = 0f;
                maxX = zoneWidth / 3f;
                minY = zoneHeight / 3f;
                maxY = 2f * zoneHeight / 3f;
                break;

            case ZoneType.Daylight:
                minX = 0f;
                maxX = zoneWidth / 3f;
                minY = 2f * zoneHeight / 3f;
                maxY = zoneHeight;
                break;

            default:
                minX = 0f;
                maxX = zoneWidth;
                minY = 0f;
                maxY = zoneHeight;
                break;
        }

        targetPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }
}
