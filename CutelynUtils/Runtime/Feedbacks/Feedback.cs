using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Feedback : MonoBehaviour {
    public virtual void Invoke(FeedbackSettings _feedbackSettings) { }
}