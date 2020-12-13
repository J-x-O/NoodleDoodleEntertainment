using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Vector3 staticCameraOffset = new Vector3(0.0f, 5.0f, -10.0f);
    [SerializeField] private string playerObjectName;                           //important to assign correct name
    [SerializeField] private Quaternion cameraRotation = Quaternion.Euler(20, 0, 0);
    [SerializeField] private float zoomFactor;
    [SerializeField] private float cameraDisplacement = 1;
    private Vector3 variableCameraOffset;
    private GameObject playerObject;
    private Vector3 playerPosition;
    private new Rigidbody playerRigidbody;
    private float smoothSpeed = 0.25f;
    private Vector3 goalPosition;
    private Vector3 smoothedPosition;

    // Start is called before the first frame update
    private void Awake()
    {
        transform.rotation = cameraRotation;
        playerObject = GameObject.Find(playerObjectName);
        playerRigidbody = playerObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        playerPosition = GameObject.Find(playerObjectName).transform.position;

        variableCameraOffset = staticCameraOffset * (Mathf.Abs(playerRigidbody.velocity.x / zoomFactor) + 1f);
        goalPosition = playerPosition + new Vector3(playerRigidbody.velocity.x / cameraDisplacement, playerRigidbody.velocity.y / cameraDisplacement, 0) + variableCameraOffset;
        smoothedPosition = Vector3.Lerp(transform.position, goalPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
