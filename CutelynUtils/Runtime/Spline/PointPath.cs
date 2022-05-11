using System.Collections;
using System.Collections.Generic;
using CaitUtils.Deprecated.Generic;
using UnityEngine;


namespace CaitUtils.Spline {
    [System.Serializable]
    public class PointPath : ScriptableObject {
        public bool IsLoop;
        public List<Vector3> Points = new List<Vector3>();
        public Vector3[] CalcWorldLocations(Transform _transform) {
            var result = new Vector3[Points.Count];
            
            for (var i = 0; i < Points.Count; i++) 
                result[i] = _transform.TransformPoint(Points[i]);
            
            return result;
        }
    }
}