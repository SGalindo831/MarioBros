using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    // public float moveSpeed = 5.0f;
    public float smoothSpeed = 0.125f;
    public float leftBoundary = 0f;
    public float rightBoundary = 0f;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        CharacterController player = FindObjectOfType<CharacterController>();
        playerTransform = player.transform;
    }

    void Update()
    {
        if (playerTransform == null)
            return;

        Vector3 position = transform.position;
        position.x = playerTransform.position.x;

        //Clamp to have boundaries
        position.x = Mathf.Clamp(position.x, leftBoundary, rightBoundary);
        transform.position = position;
    }

    // void Update()
    // {
    //     float horizontalInput = Input.GetAxis("Horizontal");

    //     if (horizontalInput != 0)
    //     {
    //         //Get new position new position
    //         Vector3 position = transform.position;
    //         position.x += horizontalInput * moveSpeed * Time.deltaTime;

    //         //Clamp to have boundaries
    //         position.x = Mathf.Clamp(position.x, leftBoundary, rightBoundary);
    //         transform.position = position;
    //     }
    // }
}