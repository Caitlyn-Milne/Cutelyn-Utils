using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaitUtils.Grid.Examples {
    public class ExampleGrid : GenericGrid<ExampleCel> {
        #region debug
        private void OnDrawGizmos() {
            foreach (GenericCel cel in Map) {
                Gizmos.DrawCube(cel.WorldLocation, Vector3.one * 0.8f);

            }
        }
        #endregion
    }
}