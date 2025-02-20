using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float leftBoundary = 0f;
    public float rightBoundary = 0f;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0)
        {
            //Get new position new position
            Vector3 position = transform.position;
            position.x += horizontalInput * moveSpeed * Time.deltaTime;

            //Clamp to have boundaries
            position.x = Mathf.Clamp(position.x, leftBoundary, rightBoundary);
            transform.position = position;
        }
    }
}