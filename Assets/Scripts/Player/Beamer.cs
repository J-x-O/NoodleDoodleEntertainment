using System;
using Unity.Mathematics;
using UnityEngine;

namespace Player {
    [RequireComponent(typeof(LineRenderer))]
    public class Beamer : MonoBehaviour {
        [SerializeField] private PlayerAim aim;
        [SerializeField] private float maxDistance = 5.0f;
        [SerializeField] private Transform origin;
        private LineRenderer lineRenderer = null;

        private void Awake() {
            lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update() {
            if (Input.GetKey(KeyCode.Mouse0)) {
                FireBeam();
            }
            if (Input.GetKey(KeyCode.Mouse1)) {
                IceBeam();
            }
        }

        public void FireBeam() {
            lineRenderer.startColor = lineRenderer.endColor = Color.red;
            var direction = aim.GetAimDirection();
            var position = origin.position;
            if (Physics.Raycast(position, direction, out var hitInfo, maxDistance)) {
                hitInfo.collider.GetComponent<Renderer>().material.color = Color.red;
                //var target = hitInfo.collider.GetComponent<IBeamTarget>();
                lineRenderer.SetPositions(new []{position, hitInfo.point}); //TODO replace
                return;
            }
            lineRenderer.SetPositions(new []{position, position + (Vector3)direction * maxDistance}); //TODO replace
        }
        
        public void IceBeam() {
            lineRenderer.startColor = lineRenderer.endColor = Color.cyan;
            var direction = aim.GetAimDirection();
            var position = origin.position;
            if (Physics.Raycast(position, direction, out var hitInfo, maxDistance)) {
                hitInfo.collider.GetComponent<Renderer>().material.color = Color.cyan;

                //var target = hitInfo.collider.GetComponent<IBeamTarget>();
                lineRenderer.SetPositions(new []{position, hitInfo.point}); //TODO replace
                return;
            }
            lineRenderer.SetPositions(new []{position, position + (Vector3)direction * maxDistance}); //TODO replace
        }
    }

    public interface IBeamTarget { }
}