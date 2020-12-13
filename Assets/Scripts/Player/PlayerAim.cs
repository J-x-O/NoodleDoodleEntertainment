using Unity.Mathematics;
using UnityEngine;

namespace Player {
    public class PlayerAim : MonoBehaviour {
        [SerializeField] private new Camera camera;
        [SerializeField] private Transform center;
        internal float3 aimDirection { get; private set; }

        private void Update() {
            var trans = transform;
            var position = trans.position;
            float3 mousePos = camera.ScreenToViewportPoint(Input.mousePosition);
            float3 playerPos = camera.WorldToViewportPoint(position);
            aimDirection = new float3(math.normalize(mousePos.xy - playerPos.xy), 0);
            trans.position = (float3)center.position + aimDirection;
            trans.rotation = Quaternion.LookRotation(aimDirection, Vector3.up);
        }
    }
}