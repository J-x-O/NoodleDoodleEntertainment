using UnityEngine;

public class CameraMovement : MonoBehaviour {
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 staticCameraOffset = new Vector3(0.0f, 5.0f, -10.0f);
    [SerializeField] private Quaternion cameraRotation = Quaternion.Euler(20, 0, 0);
    [SerializeField] private float zoomFactor = 60.0f;
    [SerializeField] private float cameraDisplacement = 1.0f;
    [SerializeField] private float smoothSpeed = 0.25f;
    private Rigidbody playerRigidbody;

    private void Awake() {
        transform.rotation = cameraRotation;
        playerRigidbody = playerTransform.GetComponent<Rigidbody>();
    }

    private void Update() {
        var playerPosition = playerTransform.position;
        var velocity = playerRigidbody.velocity;
        var variableCameraOffset = staticCameraOffset * (Mathf.Abs(velocity.x / zoomFactor) + 1.0f);
        var goalPosition = playerPosition + new Vector3(velocity.x / cameraDisplacement, velocity.y / cameraDisplacement, 0.0f) + variableCameraOffset;
        transform.position = Vector3.Lerp(transform.position, goalPosition, smoothSpeed * Time.deltaTime);
    }
}