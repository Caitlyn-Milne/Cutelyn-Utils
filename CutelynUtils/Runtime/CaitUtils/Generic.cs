using System;
using System.Collections;
using System.Collections.Generic;
using CaitUtils.Generic;
using UnityEngine;
using UnityEngine.Serialization;


namespace CaitUtils.Deprecated.Generic {
    public static class ExtensionMethods
    {
        public static float Map(this float _s, float _a1, float _a2, float _b1, float _b2)
        {
            return _b1 + (_s-_a1)*(_b2-_b1)/(_a2-_a1);
        }
    }

    public static class Generic {
        public static T[] Copy2DArrayTo1D<T>(T[,] _2dArray) {
            var maxX = _2dArray.GetLength(0);
            var maxY = _2dArray.GetLength(1);
            var _1dArray = new T[maxX * maxY];
            var i = 0;
            for (var x = 0; x < maxX; x++) {
                for (var y = 0; y < maxY; y++) {
                    _1dArray[i] = _2dArray[x, y];
                    i++;
                }
            }
            return _1dArray;
        }
        /// <summary>
        /// topdown is xz plane not xy, this uses the x and z component to create a vector2
        /// </summary>
        /// <param name="_vec3">Vector 3 whos xz values will be used to create a vector2</param>
        /// <returns>Vector2 made from the xz components of _vec3</returns>
        [Obsolete("Vector3ToVector2Topdown(Vector3), please use Vector3.ToVector2XZ instead.")]
        public static Vector2 Vector3ToVector2Topdown(Vector3 _vec3) {
            return new Vector2(_vec3.x, _vec3.z);
        }
        /// <summary>
        /// topdown is xz plane not xy, places the x and y components of a vector 2 onto the xz plane of a vector
        /// </summary>
        /// <param name="_vec2">Vector2 whos xy values will be placed onto the xz planess of a vector 3</param>
        /// <returns>the resulting vector3 when _vec2 components are placed onto the xz plane</returns>
        [Obsolete("Vector2ToVector3Topdown(Vector2), please use Vector2.ToVector3XZ instead.")]
        public static Vector3 Vector2ToVector3Topdown(Vector2 _vec2) {
            return new Vector3(_vec2.x, 0, _vec2.y);
        }

        

        public static float Noramlize01(float _value, float _min, float _max) {
            return (_value - _min) / (_max - _min);
        }
        #region Random Gaussian

        public sealed class GaussianRandom {
            private bool hasDeviate;
            private double storedDeviate;
            private readonly System.Random random;

            public GaussianRandom(System.Random _random = null) {
                this.random = _random ?? new System.Random();
            }

            /// <summary>
            /// Obtains normally (Gaussian) distributed random numbers, using the Box-Muller
            /// transformation.  This transformation takes two uniformly distributed deviates
            /// within the unit circle, and transforms them into two independently
            /// distributed normal deviates.
            /// </summary>
            /// <param name="mu">The mean of the distribution.  Default is zero.</param>
            /// <param name="sigma">The standard deviation of the distribution.  Default is one.</param>
            /// <returns></returns>
            public double NextGaussian(double _mu = 0, double _sigma = 1) {
                if (_sigma <= 0)
                    throw new System.ArgumentOutOfRangeException("_sigma", "Must be greater than zero.");

                if (hasDeviate) {
                    hasDeviate = false;
                    return storedDeviate * _sigma + _mu;
                }

                double v1, v2, rSquared;
                do {
                    // two random values between -1.0 and 1.0
                    v1 = 2 * random.NextDouble() - 1;
                    v2 = 2 * random.NextDouble() - 1;
                    rSquared = v1 * v1 + v2 * v2;
                    // ensure within the unit circle
                } while (rSquared >= 1 || rSquared == 0);

                // calculate polar tranformation for each deviate
                var polar = System.Math.Sqrt(-2 * System.Math.Log(rSquared) / rSquared);
                // store first deviate
                storedDeviate = v2 * polar;
                hasDeviate = true;
                // return second deviate
                return v1 * polar * _sigma + _mu;
            }
        }
        #endregion



        #region curves
        public static Vector3 Lerp(Vector3 _a, Vector3 _b, float _t) {
            return _a + (_b - _a) * _t;
        }
        public static Vector3 QuadraticCurve(Vector3 _a, Vector3 _b, Vector3 _c, float _t) {
            Vector3 p0 = Lerp(_a, _b, _t);
            Vector3 p1 = Lerp(_b, _c, _t);

            return Lerp(p0, p1, _t);
        }
        public static Vector3 CubicCurve(Vector3 _a, Vector3 _b, Vector3 _c, Vector3 _d, float _t) {
            Vector3 p0 = QuadraticCurve(_a, _b, _c, _t);
            Vector3 p1 = QuadraticCurve(_b, _c, _d, _t);
            return Lerp(p0, p1, _t);
        }

        public static Vector3[] BezierPath(Vector3 _startPos, Vector3 _endPos, Vector3 _startTan, Vector3 _endTan, int _amountOfSegs) {
            if (_amountOfSegs < 1) {
                return null;
            }
            float tIncriment = 1f / _amountOfSegs;
            Vector3[] path = new Vector3[_amountOfSegs + 1];

            for (int i = 0; i < _amountOfSegs + 1; i++) {
                path[i] = CubicCurve(_startPos, _startTan, _endTan, _endPos, i * tIncriment);
            }
            return path;
        }
        #endregion

        #region Gizmos
#if UNITY_EDITOR
        public static class GizmosExtended {
            public static void DrawBounds(Bounds _b, float _delay = 0) {
                // bottom
                var p1 = new Vector3(_b.min.x, _b.min.y, _b.min.z);
                var p2 = new Vector3(_b.max.x, _b.min.y, _b.min.z);
                var p3 = new Vector3(_b.max.x, _b.min.y, _b.max.z);
                var p4 = new Vector3(_b.min.x, _b.min.y, _b.max.z);

                Debug.DrawLine(p1, p2, Color.blue, _delay);
                Debug.DrawLine(p2, p3, Color.red, _delay);
                Debug.DrawLine(p3, p4, Color.yellow, _delay);
                Debug.DrawLine(p4, p1, Color.magenta, _delay);

                // top
                var p5 = new Vector3(_b.min.x, _b.max.y, _b.min.z);
                var p6 = new Vector3(_b.max.x, _b.max.y, _b.min.z);
                var p7 = new Vector3(_b.max.x, _b.max.y, _b.max.z);
                var p8 = new Vector3(_b.min.x, _b.max.y, _b.max.z);

                Debug.DrawLine(p5, p6, Color.blue, _delay);
                Debug.DrawLine(p6, p7, Color.red, _delay);
                Debug.DrawLine(p7, p8, Color.yellow, _delay);
                Debug.DrawLine(p8, p5, Color.magenta, _delay);

                // sides
                Debug.DrawLine(p1, p5, Color.white, _delay);
                Debug.DrawLine(p2, p6, Color.gray, _delay);
                Debug.DrawLine(p3, p7, Color.green, _delay);
                Debug.DrawLine(p4, p8, Color.cyan, _delay);
            }
        }

#endif
        #endregion
    }
    #region serialzable2dArray
    //i know multi dimentional arrays exsit but they arent serializable by default meaning they dont display in editor,
    //this is better for unity
    [System.Serializable]
    public class Array2D<T> : IEnumerable<T> {
        [SerializeField]
        public Array1D<T>[] Xs;
        public T this[int _x, int _y] {
            get {
                return Xs[_x][_y];
            }
            set {
                Xs[_x][_y] = value;
            }
        }
        public int LengthX {
            get {
                return Xs.Length;
            }
        }

        public int LengthY {
            get {
                try {
                    Array1D<T> index1 = Xs[0];
                    return index1.Length;
                }
                catch {
                    return 0;
                }
            }
        }
        public Array2D(int _sizeX, int _sizeY) {
            Xs = new Array1D<T>[_sizeX];
            for (int x = 0; x < _sizeX; x++) {
                Xs[x] = new Array1D<T>(_sizeY);
            }
        }
        public IEnumerator<T> GetEnumerator() {
            for (int x = 0; x < LengthX; x++) {
                Array1D<T> xArr = Xs[x];
                for (int y = 0; y < LengthY; y++) {
                    yield return this[x, y];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
    //this is needed for array 2d to be serialized, yes im aware how silly this code looks
    [System.Serializable]
    public class Array1D<T2> {
        [FormerlySerializedAs("array")]
        public T2[] Array;
        public T2 this[int _x] {
            get {
                return Array[_x];
            }
            set {
                Array[_x] = value;
            }
        }
        public int Length {
            get {
                return Array.Length;
            }
        }
        public Array1D(int _size) {
            Array = new T2[_size];
        }
    }
    #endregion
}