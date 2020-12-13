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

        private ParticleSystem particleSystem;
        private bool boosting = false;

        private void OnEnable() {
            particleSystem = GetComponentInChildren<ParticleSystem>();
            if (particleSystem == null) Debug.LogError("Add FireParticlePreset To FireThruster!");
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
            if (value) particleSystem.Play();
            else particleSystem.Stop();
            boosting = value;
        }
    }
}