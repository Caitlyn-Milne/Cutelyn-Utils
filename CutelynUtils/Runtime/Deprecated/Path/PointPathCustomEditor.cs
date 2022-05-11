using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

///Replaced with CaitUtils.Spline
namespace CaitUtils.Deprecated.Path {
    [CustomEditor(typeof(PointPath))]
    public class PointPathCustomEditor : Editor
    {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            if (GUILayout.Button("Open Path Editor")) {
                PointPathEditorWindow.Open((PointPath)target);
            }
        }
    }
}