using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

#if UNITY_EDITOR
public class InvokeEventWindow : EditorWindow {
    public UnityEvent OnInvoke;
    private static SerializedObject _serializedObject;
    private void OnEnable() {
        _serializedObject = new SerializedObject(this);
    }

    private void OnGUI() {
       _serializedObject.Update();
       EditorGUILayout.PropertyField(_serializedObject.FindProperty("OnInvoke"), true);
        _serializedObject.ApplyModifiedProperties();
        if (GUILayout.Button("Invoke Event")) {
            OnInvoke?.Invoke();
        }
    }
    [MenuItem("CaitUtils/Invoker")]
    public static void ShowWindow() {
        EditorWindow.GetWindow<InvokeEventWindow>();
    }


}
#endif