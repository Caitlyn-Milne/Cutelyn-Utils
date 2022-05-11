using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

///Replaced with CaitUtils.Spline
namespace CaitUtils.Deprecated.Path {
    public class PointEditorGraphView : GraphView
    {
        const float PIXELS_PER_UNIT = 100f;
        const bool INVERT_Y_POSITION = true;
        public PointEditorGraphView(PointPath _path) {
            //styleSheets.Add(Resources.Load<StyleSheet>("ShapeEditorGraph"));
            this.StretchToParentSize();

            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            Add(new GridBackground());

            //pan with Alt-LeftMouseButton drag/ MidleMouseButton drag
            this.AddManipulator(new ContentDragger());

            //other things that might interest you
            //this.AddManipulator(new SelectionDragger());
            //this.AddManipulator(new RectangleSelector());
            //this.AddManipulator(new ClickSelector());

            this.AddManipulator(new PointPathManipulator(_path));

            contentViewContainer.BringToFront();
            contentViewContainer.Add(new Label { name = "origin", text = "(0,0)" });

            //set the origin to the center of the window
            this.schedule.Execute(() =>
            {
                contentViewContainer.transform.position = parent.worldBound.size / 2f;
            });
        }

        public Vector2 WorldtoScreenSpace(Vector2 _pos) {
            var position = _pos * PIXELS_PER_UNIT - contentViewContainer.layout.position;
            if (INVERT_Y_POSITION) position.y = -position.y;
            return contentViewContainer.transform.matrix.MultiplyPoint3x4(position);
        }

        public Vector2 ScreenToWorldSpace(Vector2 _pos) {
            Vector2 position = contentViewContainer.transform.matrix.inverse.MultiplyPoint3x4(_pos);
            if (INVERT_Y_POSITION) position.y = -position.y;
            return (position + contentViewContainer.layout.position) / PIXELS_PER_UNIT;
        }
    }
}
