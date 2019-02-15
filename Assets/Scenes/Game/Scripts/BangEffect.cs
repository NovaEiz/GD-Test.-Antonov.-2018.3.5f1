using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nighday {

public class BangEffect : MonoBehaviour {

    [SerializeField] private ParticleSystem _particleSystem;

    private ParticleSystem.MainModule main;
    
    private void Awake() {
        main = _particleSystem.main;
    }

    public void OnParticleSystemStopped() {
        Destroy(gameObject);
    }

    public void SetColor(Color value) {
        main.startColor = value;
    }

    public void SetColor(ParticleSystem.MinMaxGradient value) {
        main.startColor = value;
    }

    public void SetStartLifeTimeParticles(float value) {
        main.startLifetime = value;
    }

}

}