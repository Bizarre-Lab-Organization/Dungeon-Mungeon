using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using UnityEditor.SceneManagement;
using System.Collections.Generic;

namespace DungeonMungeon
{
    public class TileEditor : Editor
    {
        [SerializeField] static GameObject prefabObject;

        [MenuItem("Tilemaps/Compress All")]
        static void CompressAll()
        {
            foreach (Tilemap tilemap in FindObjectsOfType(typeof(Tilemap)) as Tilemap[])
            {
                tilemap.CompressBounds();
            }
        }

        [MenuItem("Tilemaps/Get All Tilemaps")]
        static void GetAllTilemaps()
        {
            Tilemap[] tiles = Object.FindObjectsOfType(typeof(Tilemap)) as Tilemap[];
            Grid[] grids = Object.FindObjectsOfType(typeof(Grid)) as Grid[];

            foreach (Grid grid in grids)
            {
                foreach (Tilemap tilemap in tiles)
                {
                    if (!tilemap.transform.IsChildOf(grid.transform))
                    {
                        continue;
                    }

                    BoundsInt bounds = tilemap.cellBounds;
                    TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

                    GameObject go = PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/testing/GameObject.prefab", typeof(GameObject)) as GameObject) as GameObject;
                    go.name = "TILE " + tilemap.name;

                    for (int x = 0; x < bounds.size.x; x++)
                    {
                        for (int y = 0; y < bounds.size.y; y++)
                        {
                            TileBase tile = allTiles[x + y * bounds.size.x];

                            if (tile != null)
                            {
                                Vector3Int localPlace = new Vector3Int(x, y, (int)tilemap.transform.position.y);

                                GameObject til = PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/testing/Pos.prefab", typeof(GameObject)) as GameObject) as GameObject;
                                til.transform.parent = go.transform;
                                til.transform.position = (grid.GetCellCenterWorld(localPlace) + tilemap.localBounds.center) - tilemap.localBounds.size / 2;
                            }
                        }
                    }
                }
            }
        }
    }
}