using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

///Replaced with CaitUtils.Spline
namespace CaitUtils.Deprecated.Path {
    //SHOUT OUT TO https://stackoverflow.com/users/5358619/pluto on stack overflow for the base code I worked off
    //https://stackoverflow.com/questions/63557744/custom-window-mimicking-sceneview
    public class PointPathManipulator : MouseManipulator {
        private PointPath mPointPath;
        private PointPathDraw mPointPathDraw;

        public PointPathManipulator(PointPath _pointPath) {

            activators.Add(new ManipulatorActivationFilter { button = MouseButton.LeftMouse });
            mPointPath = _pointPath;
            mPointPathDraw = new PointPathDraw { PointPath = mPointPath };
        }
        protected override void RegisterCallbacksOnTarget() {
            target.Add(mPointPathDraw);
            target.Add(new Label { name = "mousePosition", text = "(0,0)" });
            target.RegisterCallback<MouseDownEvent>(MouseDown);
            target.RegisterCallback<MouseMoveEvent>(MouseMove);
            target.RegisterCallback<MouseCaptureOutEvent>(MouseOut);
            target.RegisterCallback<MouseUpEvent>(MouseUp);
        }

        protected override void UnregisterCallbacksFromTarget() {
            target.UnregisterCallback<MouseDownEvent>(MouseDown);
            target.UnregisterCallback<MouseUpEvent>(MouseUp);
            target.UnregisterCallback<MouseMoveEvent>(MouseMove);
            target.UnregisterCallback<MouseCaptureOutEvent>(MouseOut);
        }

        private void MouseOut(MouseCaptureOutEvent _evt) => mPointPathDraw.DrawSegment = false;

        private int IsInBox(Vector2 _localMosPs) {
            var t = target as PointEditorGraphView;
            const float boxSize = 15;
            for(int i = 0; i < mPointPath.LocalPoints.Count; i++) {
                Vector2 point = t.WorldtoScreenSpace(mPointPath.LocalPoints[i]);
                if (Vector2.Distance(point, _localMosPs) < boxSize) {
                    return i;
                }
            }
            return -1;
        }

        private void MouseMove(MouseMoveEvent _evt) {
            //
            var t = target as PointEditorGraphView;
            var mouseLabel = target.Q("mousePosition") as Label;
            mouseLabel.transform.position = _evt.localMousePosition + Vector2.up * 20;
            mouseLabel.text = t.ScreenToWorldSpace(_evt.localMousePosition).ToString();

            int currentPoint = IsInBox(_evt.localMousePosition);
            if (currentPoint != -1) {
                mouseLabel.text = $"Point {currentPoint}";

            }


            //if left mouse is pressed 
            if ((_evt.pressedButtons & 1) != 1) return;
            mPointPathDraw.ManipulatedPoint = t.ScreenToWorldSpace(_evt.localMousePosition);
            mPointPathDraw.MarkDirtyRepaint();

            //IF IN BOX


        }

        private void MouseUp(MouseUpEvent _evt) {
            //IF IN BOX
            if (!CanStopManipulation(_evt)) return;
            target.ReleaseMouse();
            if (!mPointPathDraw.DrawSegment) return;

            // if (mPointPath.LocalPoints.Count == 0) mPointPath.LocalPoints.Add(mPointPathDraw.start);

            var t = target as PointEditorGraphView;

            var newLocation = t!.ScreenToWorldSpace(_evt.localMousePosition);

            if (mPointPathDraw.CurrentManipulationPoint >= 0) 
                mPointPath.LocalPoints[mPointPathDraw.CurrentManipulationPoint] = newLocation;
            else 
                mPointPath.LocalPoints.Add(newLocation);
            

            mPointPathDraw.ManipulatedPoint = null;

            mPointPathDraw.DrawSegment = false;
            mPointPathDraw.CurrentManipulationPoint = -1;

            mPointPathDraw.MarkDirtyRepaint();


        }

        private void MouseDown(MouseDownEvent _evt) {

            if (!CanStartManipulation(_evt)) return;
            target.CaptureMouse();

            var t = target as PointEditorGraphView;

            mPointPathDraw.DrawSegment = true;

            mPointPathDraw.CurrentManipulationPoint = IsInBox(_evt.localMousePosition);

            if (mPointPathDraw.CurrentManipulationPoint >= 0) {
                //move point
            }
            else {
                //add point
                mPointPathDraw.ManipulatedPoint = t.ScreenToWorldSpace(_evt.localMousePosition);
                mPointPathDraw.MarkDirtyRepaint();
            }
        }
        private class PointPathDraw : ImmediateModeElement {

            public PointPath PointPath;
            public Vector2? ManipulatedPoint { get; set; }
            public bool DrawSegment { get; set; }

            public bool IsLoop = false;

            public int CurrentManipulationPoint;

            List<Vector2> Splinify(List<Vector2> _points, float _step) {
                List<Vector2> result = new List<Vector2>();
                for (float t = 0; t < (float)_points.Count; t += _step) {
                    result.Add(GetSplinePoint(_points,t, true));
                }
                return result;
            }

            Vector2 GetSplinePoint(List<Vector2> _points,float _t, bool _bLooped = false) {
                int p0, p1, p2, p3;
                if (!_bLooped) {
                    p1 = (int)_t + 1;
                    p2 = p1 + 1;
                    p3 = p2 + 1;
                    p0 = p1 - 1;
                }
                else {
                    p1 = (int) _t;
                    p2 = (p1 + 1) % _points.Count;
                    p3 = (p2 + 1) % _points.Count;
                    p0 = p1 >= 1 ? p1 - 1 : _points.Count - 1;
                }

                _t = _t - (int)_t;

                float tt = _t * _t;
                float ttt = tt * _t;

                float q1 = -ttt + 2.0f * tt - _t;
                float q2 = 3.0f * ttt - 5.0f * tt + 2.0f;
                float q3 = -3.0f * ttt + 4.0f * tt + _t;
                float q4 = ttt - tt;

                float tx = 0.5f * (_points[p0].x * q1 + _points[p1].x * q2 + _points[p2].x * q3 + _points[p3].x * q4);
                float ty = 0.5f * (_points[p0].y * q1 + _points[p1].y * q2 + _points[p2].y * q3 + _points[p3].y * q4);

                return new Vector2(tx, ty);
            }

            protected override void ImmediateRepaint() {
                List<Vector2> points = new List<Vector2>(PointPath.LocalPoints);
                if (points.Count < 1) return;

                if (ManipulatedPoint != null) {
                    if (CurrentManipulationPoint > -1) {
                        points[CurrentManipulationPoint] = (Vector2)ManipulatedPoint;
                    }
                    else {
                        points.Add((Vector2)ManipulatedPoint);
                    }
                }

                //CONVERT SPLINE POINTS TO SPLINE
                List<Vector2> splinePoints = Splinify(points, 0.1f);

                var t = parent as PointEditorGraphView;

                if (splinePoints.Count < 1) return;

                //Draw path        
                for (int i = 1; i < splinePoints.Count; i++) {
                    var p1 = t.WorldtoScreenSpace(splinePoints[i]);
                    var p2 = t.WorldtoScreenSpace(splinePoints[i -1]);
                    GL.Begin(GL.LINES);
                    GL.Color(Color.yellow);
                    GL.Vertex(p1);
                    GL.Vertex(p2);
                    GL.End();
                }
                if (PointPath.IsLoop) {
                    var p1 = t.WorldtoScreenSpace(points.Last());
                    var p2 = t.WorldtoScreenSpace(points[0]);
                    GL.Begin(GL.LINES);
                    GL.Color(Color.red);
                    GL.Vertex(p1);
                    GL.Vertex(p2);
                    GL.End();
                }



                //draw points
                float width = 10;
                for (int i = 0; i < points.Count; i++) {
                    var p1 = t.WorldtoScreenSpace(points[i]);

                    Vector2[] locations = new Vector2[4] {
                        new Vector3(p1.x - width ,p1.y + width),
                        new Vector3(p1.x + width,p1.y + width),
                        new Vector3(p1.x + width,p1.y - width),
                        new Vector3(p1.x - width,p1.y - width)
                    };
                    var prevLocation = locations[3];
                    for (int l = 0; l < locations.Length; l++) {
                        var location = locations[l];
                        GL.Begin(GL.LINES);

                        GL.Color(Color.cyan);

                        GL.Vertex(prevLocation);
                        GL.Vertex(location);
                        GL.End();
                        prevLocation = location;
                    }
                }

            }
        }
    }
}
