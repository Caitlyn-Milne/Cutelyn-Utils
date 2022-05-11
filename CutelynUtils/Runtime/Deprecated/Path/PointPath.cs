using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///Replaced with CaitUtils.Spline
namespace CaitUtils.Deprecated.Path {
    [System.Serializable]
    [CreateAssetMenu(menuName = "CaitUtils/Path/PointPaths")]
    public class PointPath : ScriptableObject {
        public bool IsLoop;
        public List<Vector2> LocalPoints = new List<Vector2>();
    }
}