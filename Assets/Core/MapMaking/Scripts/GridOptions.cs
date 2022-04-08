using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DungeonMungeon
{
    public class GridOptions : MonoBehaviour
    {
        [SerializeField] private Tile tileToReplaceWith; 

        public Tile TileToReplaceWith
        {
            get { return tileToReplaceWith; }
            set { tileToReplaceWith = value; }
        }
    }
}
