using Unity.Mathematics;
using UnityEngine;

namespace Player {
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public class PlayerMovement : MonoBehaviour {
        [SerializeField] private float jumpHeight = 8.0f;
        [SerializeField] private float accelerationMultiplier = 30.0f;
        [SerializeField] private float inAirMultiplier = 0.2f;
        [SerializeField] private float maxAirVelocity = 4.0f;
        [SerializeField] private LayerMask collisionMask = new LayerMask{value = 1 << 9};

        [SerializeField] Animator animator;

        [SerializeField] private PhysicMaterial defaultMaterial, slidingMaterial;
        private readonly Collider[] cache = new Collider[10];
        private new Collider collider;
        private bool isGrounded;
        private new Rigidbody rigidbody;
        private bool sliding;
        private float3 velocity;

        private void Awake() {
            rigidbody = GetComponent<Rigidbody>();
            collider = GetComponent<Collider>();
        }

        private void Update() {
            velocity = rigidbody.velocity;
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            var acceleration = x * accelerationMultiplier;
            var deltaTime = Time.deltaTime;
            GroundCheck();
            if (!isGrounded) {
                if (velocity.x * math.sign(acceleration) < maxAirVelocity) velocity.x += math.clamp(acceleration * inAirMultiplier * deltaTime, -maxAirVelocity, maxAirVelocity);
            }
            else {
                velocity += (float3) transform.forward * (acceleration * deltaTime);
                if (Input.GetKeyDown(KeyCode.W)) Jump();
            }

            if (Physics.Raycast(transform.position, velocity.xzz, out var hitInfo, 0.6f, collisionMask)) Bonk(hitInfo);
            rigidbody.velocity = velocity;

            //set velocity for animator
            animator.SetFloat("Velocity", velocity.x + velocity.y);
            animator.SetFloat("HorizontalVelocity", velocity.x);
            animator.SetFloat("VerticalVelocity", velocity.y);          
            
        }

        private void Jump() {
            velocity += (float3) transform.up * jumpHeight;
        }

        private void Bonk(RaycastHit hit) {
            //we hit a wall, ouch
            velocity.x = 0;
            float3 distance = transform.position - hit.point;
            if (math.lengthsq(distance) > 0.25f) transform.position = (float3) hit.point + math.normalize(distance) * 0.6f;
        }

        private void GroundCheck() {
            if (Physics.SphereCast(transform.position, 0.5f, Vector3.down, out var hitInfo, 1.02f, collisionMask)) {
                isGrounded = true;
                //var direction = math.cross(hitInfo.normal, Vector3.forward);
                //transform.rotation = quaternion.LookRotation(direction, Vector3.up);
                var size = Physics.OverlapSphereNonAlloc(hitInfo.point, 0.25f, cache, new LayerMask {value = 1024});
                if (size <= 0) {
                    ToggleSliding(false);
                    return;
                }
                for (var index = 0; index < size; index++) {
                    var hitCollider = cache[index];
                    if (hitCollider.GetComponent<IceSheet>() == null) continue;
                    ToggleSliding(true);
                    return;
                }
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0.0f, 90.0f, 0.0f), Time.deltaTime * 2.0f);
            isGrounded = false;
        }

        private void ToggleSliding(bool value) {
            if (sliding == value) return;
            sliding = value;
            if (value) {
                collider.material = slidingMaterial;
                return;
            }

            collider.material = defaultMaterial;
        }

        internal void Boost(float3 boost) {
            rigidbody.velocity += (Vector3) boost * Time.deltaTime;
        }
    }
}