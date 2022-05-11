using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


namespace CaitUtils.Grid {
    [System.Serializable]
    public class GenericCel {
        [FormerlySerializedAs("gridLocation")]
        public Vector2Int GridLocation;
        [FormerlySerializedAs("worldLocation")]
        public Vector3 WorldLocation;

        #region constructors
        public GenericCel() { }
        public GenericCel(Vector3 _worldLocation, Vector2Int _gridLocation) {
            WorldLocation = _worldLocation;
            GridLocation = _gridLocation;
        }
        #endregion
    }
}