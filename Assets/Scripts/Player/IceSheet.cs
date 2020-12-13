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

            //Testfor Freezable Stone
            if(Physics.Raycast(transform.position, -transform.forward, out var hit, 0.1f)) {
                Freezable freezable = hit.collider.GetComponent<Freezable>();
                if (freezable) freezable.freeze(duration);
            }
        }

        public void Refresh(float duration) {
            lifeTime = duration;
        }
    }
}