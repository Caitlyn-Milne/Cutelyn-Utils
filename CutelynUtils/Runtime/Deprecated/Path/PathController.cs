using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

///Replaced with CaitUtils.Spline
namespace CaitUtils.Deprecated.Path {
    public class PathController : MonoBehaviour
    {
        [FormerlySerializedAs("mPointPath")]
        public PointPath MPointPath;

        Vector3[] worldPointsCache;
        Vector3 transformPosAtWorldPointAtCache;
        Vector3 transformScaleAtWorldPointAtCache;

        /// <summary>
        /// Gets the world location of the path. A cached result is stored that changes upon transform moving
        /// </summary>
        /// <param name="recalculate">recalculate ignores the cache and recalculates the values</param>
        /// <returns>an array of vectors3 coverted from local space to world space</returns>
        public Vector3[] GetWorldPoints(bool _recalculate = false) {
            if (!_recalculate
                && transform.position == transformPosAtWorldPointAtCache 
                && transform.localScale == transformScaleAtWorldPointAtCache) 
                return worldPointsCache;

            Vector3[] worldPoints = new Vector3[MPointPath.LocalPoints.Count];
            for (int i = 0; i < MPointPath.LocalPoints.Count; i++) {
                worldPoints[i] = transform.TransformPoint(MPointPath.LocalPoints[i]);
            }

            transformPosAtWorldPointAtCache = transform.position;
            transformScaleAtWorldPointAtCache = transform.localScale;
            worldPointsCache = worldPoints;

            return worldPointsCache;
        }
        public void SetLocalPoint(Vector3 _localPoint, int _index) {
            MPointPath.LocalPoints[_index] = _localPoint;
        }

        public void NotifyPointsChanged() {
            GetWorldPoints(true);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos() {
            DrawPathLines();
        }

        public void DrawPathLines() {
            if (MPointPath.LocalPoints == null || MPointPath.LocalPoints.Count < 1) return;
            Vector3[] worldPoints = GetWorldPoints(true);
            for (int i = 1; i < worldPoints.Length; i++) {
                Gizmos.DrawLine(worldPoints[i - 1], worldPoints[i]);
            }
            if (MPointPath.IsLoop) {
                Gizmos.DrawLine(worldPoints[worldPoints.Length - 1], worldPoints[0]);
            }
        }
#endif
    }
}
