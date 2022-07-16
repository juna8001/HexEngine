using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexEngine {
    public interface IHexMap {
        public IEnumerable<IHexTile> AllBaseTiles { get; }
        public bool HasTile(HexCoords coords);
        public IHexTile GetTile(HexCoords coords);
    }
    
    /// <summary>
    /// Simple class for storing hexes in dictionary.
    /// </summary>
    public class HexMap<T> : IHexMap where T : IHexTile<T> {
        private Dictionary<HexCoords, T> _tiles = new Dictionary<HexCoords, T>();

        public IEnumerable<T> AllTiles => _tiles.Values;
        public IEnumerable<IHexTile> AllBaseTiles => AllTiles.Cast<IHexTile>();
        
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

        public bool HasTile(HexCoords coords) => _tiles.ContainsKey(coords);
        public IHexTile GetTile(HexCoords coords) => this[coords];

        public bool TryGetTile<TI>(HexCoords coords, out TI tile) where TI : T
        {
            if (_tiles.TryGetValue(coords, out T t))
            {
                if (t is TI foundTile)
                {
                    tile = foundTile;
                    return true;
                }
            }

            tile = default;
            return false;
        }
    }
}