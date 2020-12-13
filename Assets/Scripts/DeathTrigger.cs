using UnityEngine;

namespace DefaultNamespace {
    [RequireComponent(typeof(Collider))]
    public class DeathTrigger : MonoBehaviour {
        private void OnTriggerEnter(Collider other) {
            GameManager.Instance.Respawn();
        }
    }
}