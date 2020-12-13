using Unity.Mathematics;
using UnityEngine;

namespace Player {
    internal class FireThruster : MonoBehaviour {
        [SerializeField] private float maxDistance;
        [SerializeField] private PlayerAim aim;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private float boostAmount;
        [SerializeField] [Range(0.0f, 1.0f)] private float resourceCost;
        [SerializeField] private ResourceManager resourceManager;

        [SerializeField] ParticleSystem particleSystem;
        private bool boosting = false;
        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.volume = 0.2f;
        }

        private void Update() {
            if(boosting) {
                var trans = transform;
                var origin = trans.position;
                var aimDirection = aim.aimDirection;
                var distance = maxDistance;
                if (Physics.Raycast(origin, aimDirection, out var hitInfo, maxDistance)) distance = math.length(hitInfo.point - origin);
                float3 scale = trans.localScale;
                trans.localScale = new float3(scale.xy, distance);
                var cost = resourceCost * Time.deltaTime;
                playerMovement.Boost(-aimDirection * resourceManager.UseFire(cost) * boostAmount);
            }
        }

        public void Toggle(bool value) {
            if (value)
            {
                particleSystem.Play();
                audioSource.Play();
            }
            else
            {
                particleSystem.Stop();
                audioSource.Stop();
            }
            boosting = value;
        }
    }
}