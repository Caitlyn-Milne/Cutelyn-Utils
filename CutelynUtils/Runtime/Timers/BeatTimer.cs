using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CaitUtils.Timers{
    public class BeatTimer : EventTimer
    {
        public UnityEvent OnStressedBeat, OnUnstressedBeat;
        public int StressedBeat;
        public int BeatCount = -1;
        protected override void InvokeEvent() {
            base.InvokeEvent();
            BeatCount++;
            if (BeatCount >= StressedBeat) {
                BeatCount = 0;
                OnStressedBeat?.Invoke();
            }
            else{
                OnUnstressedBeat?.Invoke();
            }
        }
    }
}