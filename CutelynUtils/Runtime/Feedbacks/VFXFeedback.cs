using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VFX;

[RequireComponent(typeof(VisualEffect))]
public class VFXFeedback : Feedback
{
    public VisualEffect VisualEffect;
    
    public bool ShouldMoveTransform = false;

    public override void Invoke(FeedbackSettings _feedbackSettings) {
        if (ShouldMoveTransform) {
            transform.position = _feedbackSettings.Location;
        }
        VisualEffect.Play();
    }
}
