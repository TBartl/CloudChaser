using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumetricCloud : MonoBehaviour {
    public Color top;
    public Color bottom;
    bool initialized = false;
    ParticleSystem partSystem;
    ParticleSystem.Particle[] particles = { };
    short burstCount = 10;

    void Awake() {
        partSystem = this.GetComponent<ParticleSystem>();

        ParticleSystem.ShapeModule shapeModule = partSystem.shape;
        shapeModule.box = transform.parent.localScale;
        ParticleSystem.Burst burst = new ParticleSystem.Burst();
        short thisBurstCount = (short)(burstCount * (transform.parent.localScale.x * transform.parent.localScale.y * transform.parent.localScale.z));
        burst.minCount = thisBurstCount;
        burst.maxCount = thisBurstCount;
        ParticleSystem.Burst[] bursts = { burst };
        partSystem.emission.SetBursts(bursts);
    }

    void LateUpdate() {
        if (!initialized) {
            particles = new ParticleSystem.Particle[partSystem.particleCount];
            int numParticles = partSystem.GetParticles(particles);
            for (int i = 0; i < numParticles; i++) {
                particles[i].startColor = Color.Lerp(bottom, top, (particles[i].position.y / partSystem.shape.box.y) + .5f);
            }

            initialized = true;
            partSystem.SetParticles(particles, numParticles);
        }
    }
}
