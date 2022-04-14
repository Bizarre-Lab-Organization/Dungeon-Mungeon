using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DungeonMungeon
{
    public class GridOptions : MonoBehaviour
    {
        [SerializeField] private Tile tileToReplaceWith;
        [SerializeField] private List<Tile> wallTiles = new List<Tile>();

        public Tile TileToReplaceWith
        {
            get { return tileToReplaceWith; }
        }

        public List<Tile> WallTiles
        {
            get { return wallTiles; }
        }
    }
}
