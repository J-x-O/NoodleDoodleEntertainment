using Unity.Mathematics;
using UnityEngine;

namespace Player {
    internal class IceSheet : MonoBehaviour {
        private float lifeTime;

        private void Update() {
            lifeTime -= Time.deltaTime;
            if (lifeTime > 0) return;
            Destroy(gameObject);
        }

        public void Place(float3 position, float3 normal, float duration) {
            transform.position = position;
            transform.rotation = quaternion.LookRotation(normal, Vector3.forward);
            lifeTime = duration;
        }

        public void Refresh(float duration) {
            lifeTime = duration;
        }
    }
}