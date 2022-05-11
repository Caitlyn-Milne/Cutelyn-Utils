using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CaitUtils.Timers {
    public class CountdownTimer : MonoBehaviour {
        public float Length;
        public UnityEvent OnInvoke;

        float countDown;
        bool running;


        private void Update() {
            if (!running) return;
            countDown -= Time.deltaTime;
            if (countDown < 0) {
                Invoke();
                Destroy(this);
            }
        }

        public void StartTimer() {
            running = true;
            countDown = Length;
        }

        public void ResetTimer() {
            countDown = Length;
        }

        public void ResumeTimer() {
            running = true;
        }

        public void Pausetimer() {
            running = false;
        }
        public virtual void Invoke() {
            OnInvoke?.Invoke();
        }
    }
}