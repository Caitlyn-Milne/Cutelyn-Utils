using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackInvoker : MonoBehaviour
{
    public FeedbackSettings DefaultSettings = new FeedbackSettings();
    public List<Feedback> Feedbacks = new List<Feedback>();

    public void Invoke() {
        Invoke(DefaultSettings);
    }
    public void Invoke(FeedbackSettings _feedbackSettings) {
        foreach (var feedback in Feedbacks) {
            feedback.Invoke(_feedbackSettings);
        }
    }
}

