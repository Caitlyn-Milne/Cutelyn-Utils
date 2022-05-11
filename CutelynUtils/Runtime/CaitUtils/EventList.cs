using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.Serialization;
using static UnityEngine.Events.UnityEvent;

namespace CaitUtils.Deprecated.Generic {
    [System.Serializable]
    public class EventList<T> {
        public List<T> List = new List<T>();
        
        public int Count => List.Count;
        
        public int Capacity => List.Capacity;

        public UnityEvent OnListChanged = new UnityEvent();

        public T this[int _index] {
            get => List[_index];
            set {
                List[_index] = value;
                OnListChanged?.Invoke();
            }
        }
        
        public void Add(T _t) {
            List.Add(_t);
            OnListChanged?.Invoke();
        }
        
        public void Remove(T _t) {
            List.Remove(_t);
            OnListChanged?.Invoke();
        }
        
        public void AddRange(IEnumerable<T> _t) {
            List.AddRange(_t);
            OnListChanged?.Invoke();
        }



    }
}
