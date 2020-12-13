using System;
using UnityEngine;

namespace DefaultNamespace {
    [RequireComponent(typeof(Collider))]
    public class WinTrigger : MonoBehaviour {
        private void OnTriggerEnter(Collider other) {
            GameManager.Instance.Win();
        }
    }
}