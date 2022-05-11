using System;
using CaitUtils.Spline;
using System.Collections;
using System.Collections.Generic;
using CaitUtils.Deprecated.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

namespace CaitUtils.Spline {
    public class SplineComponent : MonoBehaviour {
        [SerializeField]
        private Spline Spline;
        
        public float Distance {
            get {
                var result = (Spline.UnitDistance.x * transform.localScale.x) + (Spline.UnitDistance.y * transform.localScale.y);
                return (result < 0)? -result : result;
            }
        }
        public float DebugDistance = 0;
        private void Update() {
            DebugDistance = Distance;
        }
        private void Awake() {
        }

        public Vector3 GetSplinePoint(float _t, bool _world = true) {
            return (_world) ?
                transform.TransformPoint(Spline.GetSplinePoint(_t))
                :Spline.GetSplinePoint(_t);
        }
        
        public Vector3 GetSplinePoint0To1(float _t, bool _world = true) {
            return (_world) ?
                transform.TransformPoint(Spline.GetSplinePoint0To1(_t))
                :Spline.GetSplinePoint0To1(_t);
        }

        public PointPath GetSmoothPointPath(float _t) {
            return Spline.GetSmoothPointPath(_t);
        }

        public bool IsLooping() {
            return Spline.IsLoop;
        }

       /* private float CalculateTotalDistance(float _step, bool _world = true) {
            float totalDistance = 0;
            var prevPoint = GetSplinePoint(0,_world);
            for (var t = _step; t < Spline.Points.Count - (Spline.IsLoop ? 0f: 3f); t += _step) {
                var currentPoint = GetSplinePoint(t, _world);
                totalDistance += Vector3.Distance(currentPoint, prevPoint);
                prevPoint = currentPoint;
            }
            _distance = totalDistance;
            return totalDistance;
        }*/
       

#if UNITY_EDITOR
        private void OnDrawGizmos() {
            //draw path
            var smoothPointPath = GetSmoothPointPath(0.01f);
            var smoothWorldPoints = smoothPointPath.CalcWorldLocations(transform);
            
            for (var i = 1; i < smoothWorldPoints.Length; i++) {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(smoothWorldPoints[i - 1], smoothWorldPoints[i]);
            }
            
            if (smoothPointPath.IsLoop) 
                Gizmos.DrawLine(smoothWorldPoints[smoothWorldPoints.Length - 1], smoothWorldPoints[0]);
            
            //draw points
            var worldPoints = Spline.CalcWorldLocations(transform);
            for (var i = 0; i < Spline.Points.Count; i++) {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(worldPoints[i], Vector3.one * 0.25f);
            }
        }
#endif
    }
}
