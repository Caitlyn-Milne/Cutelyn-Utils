using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

class ParticleFeedback : Feedback {
    public ParticleSystem ParticleSystem;
    public bool MoveParticle;

    public override void Invoke(FeedbackSettings _feedbackSettings) {
        ParticleSystem.Play();
        if (MoveParticle)
            ParticleSystem.transform.position = _feedbackSettings.Location;
    }
}