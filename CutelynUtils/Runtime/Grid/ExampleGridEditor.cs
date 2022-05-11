using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
namespace CaitUtils.Grid.Examples {
    [CustomEditor(typeof(ExampleGrid))]
    public class ExampleGridEditor : Editor {
        public override void OnInspectorGUI() {
            ExampleGrid grid = (ExampleGrid)target;


            DrawDefaultInspector();
            if (GUILayout.Button("Update")) {
                grid.NotifyChange();
            }

        }
    }
}
#endif
