using Unity.Mathematics;
using UnityEngine;

namespace Player {
    public class PlayerAim : MonoBehaviour {
        private float3 aimDirection;
        private new Camera camera;

        private void Awake() {
            camera = Camera.main;
        }

        private void Update() {
            float3 mousePos = camera.ScreenToViewportPoint(Input.mousePosition);
            var position = transform.position;
            float3 playerPos = camera.WorldToViewportPoint(position);
            aimDirection = new float3(math.normalize(mousePos.xy - playerPos.xy), 0);
        }
    }
}