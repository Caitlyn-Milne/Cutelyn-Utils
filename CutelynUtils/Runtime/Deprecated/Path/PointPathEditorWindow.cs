using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

///Replaced with CaitUtils.Spline
namespace CaitUtils.Deprecated.Path {

    //SHOUT OUT TO https://stackoverflow.com/users/5358619/pluto on stack overflow for the base code I worked off
    //https://stackoverflow.com/questions/63557744/custom-window-mimicking-sceneview
    public class PointPathEditorWindow : EditorWindow{
        private static PointEditorGraphView _pointEditorGraphView;
        private PointPath pointPath;



        private void CreateToolbar() {
            var toolbar = new Toolbar();

            var clearBtn = new ToolbarButton(() => pointPath.LocalPoints.Clear()); ;
            clearBtn.text = "Clear";

            var undoBtn = new ToolbarButton(() => pointPath.LocalPoints.RemoveAt(pointPath.LocalPoints.Count - 1));
            undoBtn.text = "Undo";

            

            var loopBtn = new ToolbarToggle();
            loopBtn.RegisterCallback<ChangeEvent<bool>>((_toggle) => pointPath.IsLoop = _toggle.newValue);
          //  loopBtn.RegisterCallback(() => pointPath.IsLoop = isLoop, UnityEngine.UIElements.TrickleDown.NoTrickleDown);
            loopBtn.text = "Loop";

            toolbar.Add(clearBtn);
            toolbar.Add(new ToolbarSpacer());
            toolbar.Add(undoBtn);
            toolbar.Add(new ToolbarSpacer());
            toolbar.Add(loopBtn);

            rootVisualElement.Add(toolbar);
        }

        private void CreateGraphView() {
            _pointEditorGraphView = new PointEditorGraphView(pointPath);
            _pointEditorGraphView.name = "Graph";
            rootVisualElement.Add(_pointEditorGraphView);
        }
        public static void Open(PointPath _pointPath) {
            PointPathEditorWindow window = GetWindow<PointPathEditorWindow>("Point Path Editor");
            window.pointPath = _pointPath;
            window.rootVisualElement.Clear();
            window.CreateGraphView();
            window.CreateToolbar();
        }
    }
}
