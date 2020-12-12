using Unity.Mathematics;
using UnityEngine;

namespace Player {
    public class PlayerAim : MonoBehaviour {
        internal float3 AimDirection { get; private set; }

        [SerializeField] private new Camera camera;
        [SerializeField] private Transform center;

        private void Update() {
            var trans = transform;
            var position = trans.position;
            float3 mousePos = camera.ScreenToViewportPoint(Input.mousePosition);
            float3 playerPos = camera.WorldToViewportPoint(position);
            AimDirection = new float3(math.normalize(mousePos.xy - playerPos.xy), 0);
            trans.position = (float3) center.position + AimDirection;
            trans.rotation = Quaternion.LookRotation(AimDirection, Vector3.up);

        }
    }
}