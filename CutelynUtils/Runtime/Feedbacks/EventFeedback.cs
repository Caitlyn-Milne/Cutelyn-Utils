using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EventFeedback : Feedback
{
    public UnityEvent OnEvent;
    public override void Invoke(FeedbackSettings _feedbackSettings) {
        try {
            OnEvent?.Invoke();
        }
        catch {
         //   Debug.LogError(e.Message);
        }
    }
}
