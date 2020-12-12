using Unity.Mathematics;
using UnityEngine;

namespace Player {
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour {
        [SerializeField] private float jumpHeight = 8.0f;
        [SerializeField] private float dragCoefficient = 4.0f;
        [SerializeField] private float accelerationMultiplier = 30.0f;
        [SerializeField] private float inAirMultiplier = 0.2f;
        [SerializeField] private LayerMask collisionMask;
        private new Rigidbody rigidbody;
        private float3 velocity;
        private bool isGrounded;
        [SerializeField] private float maxAirVelocity;

        private void Awake() {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Update() {
            velocity = rigidbody.velocity;
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            var acceleration = x * accelerationMultiplier;
            var deltaTime = Time.deltaTime;
            GroundCheck();
            if (!isGrounded) {
                if (velocity.x * math.sign(acceleration) < maxAirVelocity) {
                    velocity.x += math.clamp(acceleration * inAirMultiplier * deltaTime, -maxAirVelocity, maxAirVelocity);
                }
            }
            else {
                velocity.x -= velocity.x * (dragCoefficient * deltaTime); //TODO get drag from ground surface
                velocity.x += acceleration * deltaTime;
                if (Input.GetKeyDown(KeyCode.W)) Jump();
            }

            if (Physics.Raycast(transform.position, velocity.xzz, out var hitInfo, 0.6f, collisionMask)) Bonk(hitInfo);
            rigidbody.velocity = velocity;
        }

        private void Jump() {
            velocity.y += jumpHeight;
        }

        private void Bonk(RaycastHit hit) {
            //we hit a wall, ouch
            velocity.x = 0;
            float3 distance = transform.position - hit.point;
            if (math.lengthsq(distance) > 0.25f) {
                transform.position = (float3) hit.point + math.normalize(distance) * 0.6f;
            }
        }

        private void GroundCheck() {
            if (Physics.SphereCast(transform.position, 0.5f, Vector3.down, out var hitInfo,1.02f, collisionMask)) {
                isGrounded = true;
                return;
            }
            isGrounded = false;
        }
    }
}