using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexEngine {
    public abstract class HexMap {
        public abstract IEnumerable<IHexTile> AllBaseTiles { get; }
        public abstract bool HasTile(HexCoords coords);
        public abstract IHexTile GetTile(HexCoords coords);
    }
    
    /// <summary>
    /// Simple class for storing hexes in dictionary.
    /// </summary>
    public class HexMap<T> : HexMap where T : IHexTile<T> {
        private Dictionary<HexCoords, T> _tiles = new Dictionary<HexCoords, T>();

        public IEnumerable<T> AllTiles => _tiles.Values;
        public override IEnumerable<IHexTile> AllBaseTiles => AllTiles.Cast<IHexTile>();
        
        /// <summary>
        /// Tries to return a tile on given coords, returns null if no tile exists.
        /// </summary>
        public T this[HexCoords coords]
        {
            get
            {
                if (_tiles.TryGetValue(coords, out var result)) {
                    return result;
                }

                return default;
            }
            set
            {
                _tiles[coords] = value;
            }
        }

        public override bool HasTile(HexCoords coords) => _tiles.ContainsKey(coords);
        public override IHexTile GetTile(HexCoords coords) => this[coords];
    }
}