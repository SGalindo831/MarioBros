using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float acceleration = 3f;
    public float maxSpeed = 10f;
    public float jumpImpulse = 8f;
    public float jumpBoostForce = 8f;
    public float groundCheckDistance = 0.1f;
    public float headCheckDistance = 0.1f;

    [Header("Debug Stuff")]
    public bool isGrounded;

    private NewMonoBehaviourScript gameManager;

    Animator animator;
    Rigidbody rb;
    Collider collider;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        gameManager = FindObjectOfType<NewMonoBehaviourScript>();
    }

    void Update()
    {
        float horizontalAmount = Input.GetAxis("Horizontal");
        rb.linearVelocity += Vector3.right * horizontalAmount * Time.deltaTime * acceleration;

        float horizontalSpeed = rb.linearVelocity.x;
        horizontalSpeed = Mathf.Clamp(horizontalSpeed, -maxSpeed, maxSpeed);

        Vector3 newVelocity = rb.linearVelocity;
        newVelocity.x = horizontalSpeed;
        rb.linearVelocity = newVelocity;

        CheckIfGrounded();
        //Check if Mario hits a block with his head
        CheckHeadCollision();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpImpulse, ForceMode.VelocityChange);
        }
        else if (Input.GetKey(KeyCode.Space) && !isGrounded)
        {
            if (rb.linearVelocity.y > 0)
            {
                rb.AddForce(Vector3.up * jumpBoostForce, ForceMode.Acceleration);
            }
        }

        if (horizontalAmount == 0f)
        {
            Vector3 decayedVelocity = rb.linearVelocity;
            decayedVelocity.x *= 1f - Time.deltaTime * 4f;
            rb.linearVelocity = decayedVelocity;
        }
        else
        {
            float yawRotation = (horizontalAmount > 0f) ? 90f : -90f;
            Quaternion rotation = Quaternion.Euler(0f, yawRotation, 0f);
            transform.rotation = rotation;
        }

        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        animator.SetBool("In Air", !isGrounded);
    }

    void CheckIfGrounded()
    {
        Vector3 startPoint = transform.position;
        startPoint.y = collider.bounds.min.y + 0.01f;
        isGrounded = Physics.Raycast(startPoint, Vector3.down, groundCheckDistance);
        Debug.DrawLine(startPoint, startPoint + Vector3.down * groundCheckDistance, isGrounded ? Color.green : Color.red, 0.1f);
    }

    void CheckHeadCollision()
    {
        //Only check head collision when jumping
        if (rb.linearVelocity.y <= 0)
        {
            return;
        }

        Vector3 startPoint = transform.position;
        startPoint.y = collider.bounds.max.y - 0.01f;

        RaycastHit hit;
        bool hitSomething = Physics.Raycast(startPoint, Vector3.up, out hit, headCheckDistance);
        Debug.DrawLine(startPoint, startPoint + Vector3.up * headCheckDistance, hitSomething ? Color.yellow : Color.blue, 0.1f);

        // If we hit something
        if (hitSomething && gameManager != null)
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag("Question"))
            {
                gameManager.AddCoin();
                gameManager.AddScore(100);
                Debug.Log("Hit a question block!");
            }
            else if (hitObject.CompareTag("Brick"))
            {
                Destroy(hitObject);
                gameManager.AddScore(100);
                Debug.Log("Destroyed a brick block!");
            }
        }
    }
}