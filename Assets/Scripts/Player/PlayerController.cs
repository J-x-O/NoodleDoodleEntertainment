using UnityEngine;

namespace Player {
    public class PlayerController : MonoBehaviour {
        [SerializeField] private float maxDistance = 5.0f;
        [SerializeField] private Transform abilityOrigin;
        [SerializeField] private FireThruster fireThruster;
        [SerializeField] private IceBeam iceBeam;

        private void Update() {
            if (Input.GetKey(KeyCode.LeftShift)) {
                if (Input.GetKey(KeyCode.Mouse0)) {
                    //fireball
                }

                if (Input.GetKey(KeyCode.Mouse1)) {
                    //Icicle
                }
            }
            else {
                if (Input.GetKeyDown(KeyCode.Mouse0)) fireThruster.Toggle(true);
                if (Input.GetKeyUp(KeyCode.Mouse0)) fireThruster.Toggle(false);
                if (Input.GetKeyDown(KeyCode.Mouse1)) iceBeam.Toggle(true);
                if (Input.GetKeyUp(KeyCode.Mouse1)) iceBeam.Toggle(false);
            }
        }
    }

}