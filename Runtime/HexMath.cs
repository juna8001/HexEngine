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
            return RoundAxialToHex(PointToAxial(point, gridScale));
        }

        /// <summary>
        /// Converts 3D point to Vector2 containing axial position.
        /// </summary>
        public static Vector2 PointToAxial(Vector3 point, float gridScale = 1f) {
            var x = B * point.x;
            var y = (A * point.x + point.z);
            return new Vector2(x, y) / gridScale;
        }

        /// <summary>
        /// Rounds axial coordinates and converts them to the HexCoords.
        /// </summary>
        public static HexCoords RoundAxialToHex(Vector2 axial) {
            var x = axial.x;
            var y = axial.y;
            var z = -axial.x - axial.y;
            var xr = Mathf.RoundToInt(axial.x);
            var yr = Mathf.RoundToInt(axial.y);
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