using System;
using System.Collections.Generic;
using UnityEngine;

namespace HexEngine {
    /// <summary>
    /// Enum representation of six direction in hex space.
    /// </summary>
    public enum HexDirection
    {
        N,
        NE,
        SE,
        S,
        SW,
        NW
    }

    public static class HexDirectionExtension {
        
        #region Readonly Arrays
        private static readonly HexCoords[] _coords = new[] {
            new HexCoords(0, 1),
            new HexCoords(1, 0),
            new HexCoords(1, -1),
            new HexCoords(0, -1),
            new HexCoords(-1, 0),
            new HexCoords(-1, 1),
        };

        private static readonly float[] _angles = new[] {
            0f, 60f, 120f,
            180f, 240f, 300f
        };

        private static readonly Vector3[] _directions = new[] {
            Quaternion.Euler(0f, _angles[0], 0f) * Vector3.forward,
            Quaternion.Euler(0f, _angles[1], 0f) * Vector3.forward,
            Quaternion.Euler(0f, _angles[2], 0f) * Vector3.forward,
            Quaternion.Euler(0f, _angles[3], 0f) * Vector3.forward,
            Quaternion.Euler(0f, _angles[4], 0f) * Vector3.forward,
            Quaternion.Euler(0f, _angles[5], 0f) * Vector3.forward,
        };

        private static readonly Vector3[] _vertexDirections = new[] {
            Quaternion.Euler(0f, _angles[0] - 30f, 0f) * Vector3.forward,
            Quaternion.Euler(0f, _angles[1] - 30f, 0f) * Vector3.forward,
            Quaternion.Euler(0f, _angles[2] - 30f, 0f) * Vector3.forward,
            Quaternion.Euler(0f, _angles[3] - 30f, 0f) * Vector3.forward,
            Quaternion.Euler(0f, _angles[4] - 30f, 0f) * Vector3.forward,
            Quaternion.Euler(0f, _angles[5] - 30f, 0f) * Vector3.forward,
        };

        private const float VertexFactor = .5f * HexMath.InToOutRadius;
        private static readonly Vector3[] _vertices = new[] {
            Quaternion.Euler(0f, _angles[0] - 30f, 0f) * Vector3.forward * VertexFactor,
            Quaternion.Euler(0f, _angles[1] - 30f, 0f) * Vector3.forward * VertexFactor,
            Quaternion.Euler(0f, _angles[2] - 30f, 0f) * Vector3.forward * VertexFactor,
            Quaternion.Euler(0f, _angles[3] - 30f, 0f) * Vector3.forward * VertexFactor,
            Quaternion.Euler(0f, _angles[4] - 30f, 0f) * Vector3.forward * VertexFactor,
            Quaternion.Euler(0f, _angles[5] - 30f, 0f) * Vector3.forward * VertexFactor,
        };
        #endregion

        /// <summary>
        /// HexCoords of the direction.
        /// </summary>
        public static HexCoords Coords(this HexDirection direction) => _coords[(int) direction];

        /// <summary>
        /// Angle in degrees from 0 for HexDirection.N, increasing clockwise.
        /// </summary>
        public static float Angle(this HexDirection direction) => _angles[(int) direction];

        /// <summary>
        /// Normalized vector of direction in 3D space for HexDirection, where HexDirection.N is Vector3.forward.
        /// </summary>
        public static Vector3 Direction(this HexDirection direction) => _directions[(int) direction];
        
        /// <summary>
        /// Normalized vector of direction to the left corner of the hex edge in given direction.
        /// </summary>
        public static Vector3 LeftCornerDirection(this HexDirection direction) => _vertexDirections[(int) direction];
        
        /// <summary>
        /// Normalized vector of direction to the right corner of the hex edge in given direction.
        /// </summary>
        public static Vector3 RightCornerDirection(this HexDirection direction) => _vertexDirections[((int) direction + 1) % 6];
        
        /// <summary>
        /// Vector from the center of the hex to the left corner on the edge in given direction.
        /// </summary>
        public static Vector3 LeftCorner(this HexDirection direction) => _vertices[(int) direction];
        
        /// <summary>
        /// Vector from the center of the hex to the right corner on the edge in given direction.
        /// </summary>
        public static Vector3 RightCorner(this HexDirection direction) => _vertices[((int) direction + 1) % 6];

        /// <summary>
        /// Iterates through all hex directions starting from this direction and going clockwise.
        /// </summary>
        public static IEnumerable<HexDirection> Loop(this HexDirection direction) {
            var startIndex = (int) direction;
            for (int i = 0; i < 6; i++) {
                yield return (HexDirection) ((startIndex + i) % 6);
            }
        }

        /// <summary>
        /// Returns HexDirection rotated clockwise.
        /// </summary>
        public static HexDirection Rotate(this HexDirection direction, int rotation) {
            var value = (int) direction + rotation;
            value = (value % 6) + 6;
            return (HexDirection) (value % 6);
        }

        /// <summary>
        /// Returns HexDirection rotated clockwise once.
        /// </summary>
        public static HexDirection Right(this HexDirection direction) {
            return (HexDirection) (((int) direction + 1) % 6);
        }

        /// <summary>
        /// Returns HexDirection rotated counter-clockwise once.
        /// </summary>
        public static HexDirection Left(this HexDirection direction) {
            return (HexDirection) (((int) direction + 5) % 6);
        }

        /// <summary>
        /// Returns the opposite HexDirection.
        /// </summary>
        public static HexDirection Opposite(this HexDirection direction) {
            return (HexDirection) (((int) direction + 3) % 6);
        }

        /// <summary>
        /// If given HexCoords are coords of any HexDirection, it returns given HexDirection.
        /// Otherwise it throws an error.
        /// </summary>
        public static HexDirection ToDirection(this HexCoords coords) {
            return (HexDirection) Array.IndexOf(_coords, coords);
        }
    }
}