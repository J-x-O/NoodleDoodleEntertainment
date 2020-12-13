using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGravitation : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particles;

    [SerializeField] float speed = 2.6f;

    [SerializeField] Transform gravityCenter;

    private void Initialize() {
        particleSystem = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[particleSystem.maxParticles];
    }

    private void OnEnable() {
        Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        var deltaTime = Time.deltaTime;

        int length = particleSystem.GetParticles(particles);
        for (int i = 0; i < length; i++) {
            var position = particles[i].position;
            var direction = (gravityCenter.position - position).normalized;
            direction *= speed * deltaTime;
            particles[i].position += direction;
        }
        particleSystem.SetParticles(particles, length);
    }
}

/** 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGravitation : MonoBehaviour
{
    [SerializeField] ParticleSystem[] particleSystem;
    private ParticleSystem.Particle[][] particles;

    [SerializeField] float speed = 2.6f;

    [SerializeField] Transform gravityCenter;

    private void Initialize() {
        particles = new ParticleSystem.Particle[particleSystem.Length][];
        for (int i = 0; i < particleSystem.Length; i++) {
            particles[i] = new ParticleSystem.Particle[particleSystem[i].maxParticles];
        }
    }

    private void OnEnable() {
        Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        var deltaTime = Time.deltaTime;

        for (int p = 0; p < particleSystem.Length; p++) {
            int length = particleSystem[p].GetParticles(particles[p]);
            for (int i = 0; i < length; i++) {
                var position = particles[p][i].position;
                var direction = (gravityCenter.position - position).normalized;
                direction *= speed * deltaTime;
                particles[p][i].position += direction;
            }
            particleSystem[p].SetParticles(particles[p]);
        }
    }
}

**/
