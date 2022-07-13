using UnityEngine;

namespace HexEngine {
    public static class HexMath {
        public const float OutToInRadius = 0.866025404f;
        public const float InToOutRadius = 1.15470052f;

        private const float Sqrt3 = 1.73205080757f;
        private const float A = -Sqrt3 / 3f;
        private const float B = (2f * Sqrt3) / 3f;

        /// <summary>
        /// Returns HexCoords of hex on given point (ignoring point.y value).
        /// </summary>
        public static HexCoords PointToHexCoords(Vector3 point, float gridScale = 1f) {
            return RoundPositionToCoords(PointToHexPosition(point, gridScale));
        }

        /// <summary>
        /// Converts 3D point to Vector2 containing axial position.
        /// </summary>
        public static HexPosition PointToHexPosition(Vector3 point, float gridScale = 1f) {
            var x = B * point.x;
            var y = (A * point.x + point.z);
            return new HexPosition(x / gridScale, y / gridScale);
        }

        /// <summary>
        /// Rounds axial coordinates and converts them to the HexCoords.
        /// </summary>
        public static HexCoords RoundPositionToCoords(HexPosition position) {
            var x = position.X;
            var y = position.Y;
            var z = position.Z;
            var xr = Mathf.RoundToInt(x);
            var yr = Mathf.RoundToInt(y);
            var zr = Mathf.RoundToInt(z);

            var diffX = Mathf.Abs(x - xr);
            var diffY = Mathf.Abs(y - yr);
            var diffZ = Mathf.Abs(z - zr);

            if (diffX > diffY && diffX > diffZ) {
                xr = -yr - zr;
            }
            else if (diffY > diffZ) {
                yr = -xr - zr;
            }
            
            return new HexCoords(xr, yr);
        }
    }
}