using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace Player {
    public class ResourceManager : MonoBehaviour {
        [SerializeField] private Slider slider;
        [SerializeField] private float balanceBonusMultiplier;
        private float heat = 0.5f;
        private float Power => 1.0f + (1.0f - math.distance(heat, 1.0f - heat)) * balanceBonusMultiplier;

        internal float UseFire(float amount) {
            AddHeat(amount);
            return Power;
        }

        internal float UseIce(float amount) {
            AddHeat(-amount);
            return Power;
        }

        private void AddHeat(float value) {
            heat = math.clamp(heat + value, 0.0f, 1.0f);
            slider.value = heat;
            
        }

        public float getHeat() {
            return heat;
        }

        public float getPower() {
            return Power;
        }
    }
}