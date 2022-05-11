using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CaitUtils.Deprecated.Generic;
using UnityEngine.Serialization;

namespace CaitUtils.Grid {
    [ExecuteAlways]
    public class GenericGrid<TCel> : MonoBehaviour
        where TCel : GenericCel, new() {
        [FormerlySerializedAs("map")]
        public Array2D<TCel> Map;

        [FormerlySerializedAs("startingLocation")]
        public Vector3 StartingLocation = Vector3.zero;


        [FormerlySerializedAs("gridSize")]
        [Min(0)]
        public Vector2Int GridSize;


        [FormerlySerializedAs("celSpacing")]
        [Min(0)]
        public float CelSpacing = 1;

        public void InitalizeGrid() {
            Map = new Array2D<TCel>(1, 1);

            NotifyChange();
        }
        /// <summary>
        /// call to notify the grid of general changed parameters
        /// </summary>
        public void NotifyChange() {
            NotifyReizeGrid();
            UpdateCelsWorldLocation();
        }
        /// <summary>
        /// creates new map, copies exsiting data, and then fills empty parts of the map
        /// </summary>
        public void NotifyReizeGrid() {
            Array2D<TCel> newMap = new Array2D<TCel>(GridSize.x, GridSize.y);

            foreach (TCel cel in Map) {
                if (cel != null
                    && cel.GridLocation.x < newMap.LengthX
                    && cel.GridLocation.y < newMap.LengthY) {
                    newMap[cel.GridLocation.x, cel.GridLocation.y] = cel;
                }
            }
            Map = newMap;

            for (int x = 0; x < Map.LengthX; x++) {
                for (int y = 0; y < Map.LengthY; y++) {
                    if (Map[x, y] == null) {
                        Map[x, y] = new TCel();
                        Map[x, y].GridLocation = new Vector2Int(x, y);
                    }
                }
            }

        }
        /// <summary>
        /// Updates world locations of cells
        /// </summary>
        public void UpdateCelsWorldLocation() {
            for (int x = 0; x < Map.LengthX; x++) {
                for (int y = 0; y < Map.LengthY; y++) {
                    if (Map[x, y] != null) {
                        Map[x, y].WorldLocation = StartingLocation + new Vector3(x * (CelSpacing), 0, y * (CelSpacing));
                    }
                }
            }
        }
        public TCel GetCelFromGridLocaiton(int _x, int _y) {
            return Map[_x, _y];
        }
        public TCel GetCelFromWorldPoint(Vector3 _point) {
            Vector3 celLocation = (_point - StartingLocation) / CelSpacing;
            Vector2Int celLocationInt = new Vector2Int(Mathf.RoundToInt(celLocation.x), Mathf.RoundToInt(celLocation.z));
            return GetCelFromGridLocaiton(Mathf.RoundToInt(celLocation.x), Mathf.RoundToInt(celLocation.z));
        }

        #region Unity funcs
        private void Awake() {
            Debug.Log("awake");
            InitalizeGrid();
        }
        private void FixedUpdate() {
            // NotifyChange();
        }

        #endregion
    }
}