using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioFeedback : Feedback
{
    [FormerlySerializedAs("audioSource")]
    public AudioSource AudioSource;
    public override void Invoke(FeedbackSettings _feedbackSettings) {
        AudioSource.Play();
    }
}
