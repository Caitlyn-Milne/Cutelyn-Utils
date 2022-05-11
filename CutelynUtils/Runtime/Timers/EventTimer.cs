using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Threading;

namespace CaitUtils.Timers {
    public class EventTimer : MonoBehaviour {

        public UnityEvent OnTickEvent;
        float countDown;
        public float WaitTime;


        private void Update() {
            countDown -= Time.deltaTime;
            if (countDown < 0) {
                countDown += WaitTime;
                InvokeEvent();
            }
        }
        protected virtual void InvokeEvent() {
            OnTickEvent?.Invoke();
        }
    }

}