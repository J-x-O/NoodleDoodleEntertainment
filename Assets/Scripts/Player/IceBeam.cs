using Unity.Mathematics;
using UnityEngine;

namespace Player {
    internal class IceBeam : MonoBehaviour {
        [SerializeField] private float maxDistance;
        [SerializeField] private PlayerAim aim;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private float boostAmount;

        private void Update() {
            var trans = transform;
            var origin = trans.position;
            var aimDirection = aim.AimDirection;
            var distance = maxDistance;
            if (Physics.Raycast(origin, aimDirection ,out var hitInfo, maxDistance)) {
                distance = math.length(hitInfo.point - origin);
            }

            float3 scale = trans.localScale;
            trans.localScale = new float3(scale.xy, distance);
            playerMovement.Boost(aimDirection * boostAmount);
        }
        
        public void Toggle(bool value) {
            gameObject.SetActive(value); //TODO replace with particle system
        }
    }
}