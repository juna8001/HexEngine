using System;
using UnityEngine;

namespace HexEngine {
    [Serializable]
    public struct HexPosition : IEquatable<HexPosition> {
        public static readonly HexPosition Zero = new HexPosition();

        [SerializeField]
        private float _x;
        [SerializeField]
        private float _y;
        
        public float X {
            get => _x;
            set => _x = value;
        }

        public float Y {
            get => _y;
            set => _y = value;
        }

        public float Z => -X - Y;

        public HexPosition(float x, float y) {
            _x = x;
            _y = y;
        }
        
        public static HexPosition operator +(HexPosition a, HexPosition b)
            => new HexPosition(a.X + b.X, a.Y + b.Y);

        public static HexPosition operator +(HexPosition a, HexDirection b)
            => a + b.Coords();
        
        public static HexPosition operator -(HexPosition a, HexPosition b)
            => new HexPosition(a.X - b.X, a.Y - b.Y);

        public static HexPosition operator -(HexPosition a, HexDirection b)
            => a - b.Coords();
        
        public static implicit operator HexPosition(HexCoords coords) => new HexPosition(coords.X, coords.Y);

        /// <summary>
        /// The offset from the Vector3.zero to the center of the hex.
        /// </summary>
        public Vector3 Offset() {
            return HexDirection.NE.Direction() * X + HexDirection.N.Direction() * Y;
        }

        public bool Equals(HexPosition other) {
            return other.X == X && other.Y == Y;
        }

        public override bool Equals(object obj) {
            return obj is HexCoords coords && Equals(coords);
        }
        
        public override int GetHashCode() {
            return Tuple.Create(_x, _y).GetHashCode();
        }

        public override string ToString() {
            return $"({X}, {Y}, {Z})";
        }

        public static bool operator ==(HexPosition a, HexPosition b) {
            return a.Equals(b);
        }

        public static bool operator !=(HexPosition a, HexPosition b) {
            return !(a == b);
        }
    }
}