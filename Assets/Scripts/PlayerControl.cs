using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    public float growthFactor = 0.01f; // How much the fish grows per eaten animal

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement = movement.normalized;
    }

    void FixedUpdate()
    {
        // Move the fish using Rigidbody2D
        rb.linearVelocity = movement * moveSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Animal"))
        {
            if (transform.localScale.x > collision.gameObject.transform.localScale.x)
            {
                GrowFish();
                Destroy(collision.gameObject);
            }
            else
            {
                Destroy(gameObject);
                //load death screen here
            }
        }
    }

    void GrowFish()
    {
        transform.localScale += new Vector3(growthFactor, growthFactor, 0);
        moveSpeed += .01f;
    }
}