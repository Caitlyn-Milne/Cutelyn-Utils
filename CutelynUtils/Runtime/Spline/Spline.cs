using System;
using System.Collections;
using System.Collections.Generic;
using CaitUtils.Deprecated.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CaitUtils.Spline
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "CaitUtils/Spline/New Spline")]
    public class Spline : PointPath {

        /// <summary>
        /// vertical and horizontal distance where scale is 1
        /// </summary>
        public Vector2 UnitDistance { get; private set; } = Vector2.one;

        private void OnValidate() {
            UnitDistance = CalculateUnitDistance(0.001f);
        }

        //Converts the spline to a smooth point path
        public PointPath GetSmoothPointPath(float _step) {
            var result = ScriptableObject.CreateInstance<PointPath>();
            result.Points = new List<Vector3>();
            result.IsLoop = IsLoop;
            for (float t = 0; t < (float)Points.Count -((IsLoop)? 0: 3); t += _step) {
                result.Points.Add(GetSplinePoint(t));
            }
            
            return result;
        }

        /// <summary>
        /// Gets point along the spline at t location
        /// </summary>
        /// <param name="_t">the location along the spline, starting at 0 and ending at one</param>
        /// <returns>vector representing the location along the spline</returns>
        public Vector3 GetSplinePoint0To1(float _t) {
            _t = _t.Map(0, 1.01f, 0, Points.Count - (IsLoop? 0: 3));
            return GetSplinePoint(_t);
        }
        
        private Vector2 CalculateUnitDistance(float _step) {
            Vector2 result = Vector2.zero;
            var prevPoint = GetSplinePoint(0);
            for (var t = _step; t <Points.Count - (IsLoop ? 0f: 3f); t += _step) {
                var currentPoint = GetSplinePoint(t);
                
                result.x += (currentPoint.x > prevPoint.x) ? 
                    currentPoint.x - prevPoint.x : 
                    prevPoint.x - currentPoint.x;
                result.y += (currentPoint.y > prevPoint.y) ? 
                    currentPoint.y - prevPoint.y :
                    prevPoint.y - currentPoint.y;
                
                prevPoint = currentPoint;
            }
            return result;
        }

        /// <summary>
        /// Gets point along the spline at t location
        /// </summary>
        /// <param name="_t">the location along the spline, capping at length of localPoints array</param>
        /// <returns>vector representing the location along the spline</returns>
        public Vector3 GetSplinePoint(float _t) {
            int p0, p1, p2, p3;
            if (!IsLoop) {
                p1 = (int)_t + 1;
                p2 = p1 + 1;
                p3 = p2 + 1;
                p0 = p1 - 1;
            }
            else {
                p1 = (int)_t;
                p2 = (p1 + 1) % Points.Count;
                p3 = (p2 + 1) % Points.Count;
                p0 = p1 >= 1 ? p1 - 1 : Points.Count - 1;
            }

            _t = _t - (int)_t;

            var tt = _t * _t;
            var ttt = tt * _t;

            var q1 = -ttt + 2.0f * tt - _t;
            var q2 = 3.0f * ttt - 5.0f * tt + 2.0f;
            var q3 = -3.0f * ttt + 4.0f * tt + _t;
            var q4 = ttt - tt;

            var tx = 0.5f * (Points[p0].x * q1 + Points[p1].x * q2 + Points[p2].x * q3 + Points[p3].x * q4);
            var ty = 0.5f * (Points[p0].y * q1 + Points[p1].y * q2 + Points[p2].y * q3 + Points[p3].y * q4);
            var tz = 0.5f * (Points[p0].z * q1 + Points[p1].z * q2 + Points[p2].z * q3 + Points[p3].z * q4);

            return new Vector3(tx, ty, tz);
        }
    }


}
