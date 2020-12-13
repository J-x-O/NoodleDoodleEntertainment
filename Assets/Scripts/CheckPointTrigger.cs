using UnityEngine;

namespace DefaultNamespace {
    [RequireComponent(typeof(Collider))]
    public class CheckPointTrigger : MonoBehaviour {
        [SerializeField] private Transform checkPoint;

        private void OnTriggerEnter(Collider other) {
            GameManager.Instance.SetCheckpoint(checkPoint);
        }
    }
}