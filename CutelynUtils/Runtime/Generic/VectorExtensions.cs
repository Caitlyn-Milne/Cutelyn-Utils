
using UnityEngine;

namespace CaitUtils.Generic {
    
    //Vector 3 Extensions
    public static partial class VectorExtensions {
        
        public static Vector2 ToVector2XY(this Vector3 _vec) {
            return new Vector2(_vec.x, _vec.y);
        }
        
        public static Vector2 ToVector2XZ(this Vector3 _vec) {
            return new Vector2(_vec.x, _vec.z);
        }
        
        public static Vector2 ToVector2YZ(this Vector3 _vec) {
            return new Vector2(_vec.y, _vec.z);
        }

        public static Vector3 AddAll(this Vector3 _vec, float _x) {
            return new Vector3(_vec.x + _x, _vec.y + _x, _vec.z + _x);
        }
        
        public static Vector3 SubAll(this Vector3 _vec, float _x) {
            return new Vector3(_vec.x - _x, _vec.y - _x, _vec.z - _x);
        }

    }
    
    //Vector 2 Extensions
    public static partial class VectorExtensions {
        
        public static Vector3 ToVector3XY(this Vector2 _vec) {
            return new Vector3(_vec.x, _vec.y,0);
        }
        
        public static Vector3 ToVector3XZ(this Vector2 _vec) {
            return new Vector3(_vec.x, 0,_vec.y);
        }
        
        public static Vector3 ToVector3YZ(this Vector2 _vec) {
            return new Vector3(0, _vec.x,_vec.y);
        }
        
    }
}
