using Unity.Mathematics;
using UnityEngine;

namespace Player {
    internal class IceBeam : MonoBehaviour {
        [SerializeField] private float maxDistance;
        [SerializeField] private PlayerAim aim;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private float boostAmount;
        [SerializeField] [Range(0.0f, 1.0f)] private float resourceCost;
        [SerializeField] private ResourceManager resourceManager;
        [SerializeField] private float iceDuration;
        [SerializeField] private IceSheet iceSheetPrefab;
        [SerializeField] private LayerMask collisionMask;
        private readonly Collider[] cache = new Collider[8];

        [SerializeField] ParticleSystem particleSystem1, particleSystem2;
        private float normalEmision, normalSize;
        private bool boosting, subemitter = false;
        private void OnEnable() {
            normalEmision = particleSystem1.emissionRate;
            normalSize = particleSystem1.startSize;
        }
        public AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.volume = 0.2f;
        }

        private void Update() {
            if(boosting) {
                Transform trans = transform;
                Vector3 origin = trans.position;
                Vector3 aimDirection = aim.aimDirection;
                float distance = maxDistance;
                if (Physics.Raycast(origin, aimDirection, out var hitInfo, maxDistance, collisionMask)) {
                    Vector3 hitPosition = hitInfo.point;

                    particleSystem1.transform.position = hitPosition;
                    particleSystem1.transform.forward = hitInfo.normal;
                    particleSystem2.transform.position = hitPosition;
                    particleSystem2.transform.forward = hitInfo.normal;
                    if (!subemitter) {
                        subemitter = true;
                        particleSystem2.Play();
                    }

                    distance = math.length(hitPosition - origin);
                    int size = Physics.OverlapSphereNonAlloc(hitPosition, 0.25f, cache, new LayerMask { value = 1024 });
                    if (size > 0) {
                        for (int i = 0; i < size; i++) {
                            IceSheet iceSheet = cache[i].GetComponent<IceSheet>();
                            iceSheet.Refresh(iceDuration);


                        }
                    }
                    else {
                        IceSheet iceSheet = Instantiate(iceSheetPrefab);
                        iceSheet.Place(hitPosition, hitInfo.normal, iceDuration);
                    }
                }
                else {
                    particleSystem1.transform.position = origin + aimDirection*maxDistance;
                    particleSystem1.transform.rotation = Quaternion.identity;
                    subemitter = false;
                    particleSystem2.Stop();
                }

                float3 scale = trans.localScale;
                trans.localScale = new float3(scale.xy, distance);
                var cost = resourceCost * Time.deltaTime;
                playerMovement.Boost(aimDirection * resourceManager.UseIce(cost) * boostAmount);

                particleSystem1.emissionRate = normalEmision / 4 + (normalEmision * 3 / 4 * ((resourceManager.getPower() - 1) / 2));
                particleSystem1.startSize = normalSize / 4 + (normalSize * 3 / 4 * ((resourceManager.getPower() - 1) / 2));

                
            }
        }

        public void Toggle(bool value) {
            if (value)
            {
                particleSystem1.Play();
                audioSource.Play();
            }
            else
            {
                particleSystem1.Stop();
                particleSystem2.Stop();
                audioSource.Stop();
            }
            boosting = value;
        }
    }
}