using Unity.Mathematics;
using UnityEngine;

namespace Player {
    internal class IceBeam : MonoBehaviour {
        [SerializeField] private float maxDistance;
        [SerializeField] private PlayerAim aim;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private float boostAmount;
        [SerializeField] [Range(0.0f, 1.0f)] private float resourceCost;
        [SerializeField] private ResourceManager resourceManager;
        [SerializeField] private float iceDuration;
        [SerializeField] private IceSheet iceSheetPrefab;
        [SerializeField] private LayerMask collisionMask;
        private readonly Collider[] cache = new Collider[8];

        private void Update() {
            var trans = transform;
            var origin = trans.position;
            var aimDirection = aim.AimDirection;
            var distance = maxDistance;
            if (Physics.Raycast(origin, aimDirection, out var hitInfo, maxDistance, collisionMask)) {
                var hitPosition = hitInfo.point;
                distance = math.length(hitPosition - origin);
                var size = Physics.OverlapSphereNonAlloc(hitPosition, 0.25f, cache, new LayerMask {value = 1024});
                if (size > 0) {
                    for (var i = 0; i < size; i++) {
                        var iceSheet = cache[i].GetComponent<IceSheet>();
                        iceSheet.Refresh(iceDuration);
                    }
                }
                else {
                    var iceSheet = Instantiate(iceSheetPrefab);
                    iceSheet.Place(hitPosition, hitInfo.normal, iceDuration);
                }
            }

            float3 scale = trans.localScale;
            trans.localScale = new float3(scale.xy, distance);
            var cost = resourceCost * Time.deltaTime;
            playerMovement.Boost(aimDirection * resourceManager.UseIce(cost) * boostAmount);
        }

        public void Toggle(bool value) {
            gameObject.SetActive(value); //TODO replace with particle system
        }
    }
}