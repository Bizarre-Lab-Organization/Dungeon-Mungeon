# if (UNITY_EDITOR)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using System;
using System.Linq;

namespace DungeonMungeon
{
    [ExecuteInEditMode]
    public class testtileevent : MonoBehaviour
    {
        private GameObject selectedObject;

        private static int currentTilemapCount;
        private static int previousTilemapCount;
        private static Tilemap currentTilemap;

        [RuntimeInitializeOnLoadMethod]
        [ExecuteInEditMode]
        private void OnEnable()
        {
            selectedObject = Selection.activeGameObject;

            try
            {
                if (selectedObject.GetComponent<Tilemap>() != null)
                {
                    currentTilemap = selectedObject.GetComponent<Tilemap>();
                    currentTilemapCount = GetAmoutOfTilesInTilemap(currentTilemap);
                    previousTilemapCount = GetAmoutOfTilesInTilemap(currentTilemap);
                }
            }
            catch (MissingReferenceException e) { }
            catch (NullReferenceException e) { }

            Tilemap.tilemapTileChanged += delegate (Tilemap tilemap, Tilemap.SyncTile[] tiles)
            {
                Debug.Log("Amount of tiles that have been changed: " + tiles.Length);
     
                previousTilemapCount = currentTilemapCount;
                currentTilemapCount = GetAmoutOfTilesInTilemap(tilemap);

                if (currentTilemapCount > previousTilemapCount)
                {
                    if (EditorPrefs.GetBool("Automatic Transparentable") == true)
                    {
                        Grid grid = tilemap.transform.parent.GetComponent<Grid>();

                        foreach (var tile in tiles)
                        {
                            Vector3 pos = grid.GetCellCenterWorld(tile.position);

                            GameObject til = PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/Core/MapMaking/Level_test/Prefabs/ScrachedWall.prefab", typeof(GameObject)) as GameObject) as GameObject;
                            til.transform.parent = tilemap.transform;
                            til.transform.position = pos;

                            tilemap.SetTile(tile.position, grid.gameObject.GetComponent<GridOptions>().TileToReplaceWith);
                        }
                    }
                } else if (currentTilemapCount < previousTilemapCount)
                {
                    Grid grid = tilemap.transform.parent.GetComponent<Grid>();

                    foreach (var tile in tiles)
                    {
                        Vector3 pos = grid.GetCellCenterWorld(tile.position);

                        foreach (Transform prefab in tilemap.transform.Cast<Transform>().ToList())
                        {
                            if (prefab.position == pos)
                            {
                                DestroyImmediate(prefab.gameObject);
                                tilemap.SetTile(tile.position, null);

                                break;
                            }
                        }
                    }
                } else
                {
                    Debug.Log("a tile has been swaped wit another (can be plural)");

                    Grid grid = tilemap.transform.parent.GetComponent<Grid>();

                    foreach (var tile in tiles)
                    {
                        Vector3 pos = grid.GetCellCenterWorld(tile.position);

                        foreach (Transform prefab in tilemap.transform.Cast<Transform>().ToList())
                        {
                            if (prefab.position == pos)
                            {
                                TileBase placedTile = tilemap.GetTile(tilemap.WorldToCell(prefab.transform.position));

                                if (placedTile.name != prefab.name)
                                {
                                    DestroyImmediate(prefab.gameObject);

                                    break;
                                }
                            }
                        }
                    }
                }

                //Debug.Log("Total amount of tiles (previously): " + previousTilemapCount);
                //Debug.Log("Total amount of tiles: " + currentTilemapCount);

                currentTilemap = tilemap;
            };
        }
        
        [RuntimeInitializeOnLoadMethod]
        [ExecuteInEditMode]
        void Update()
        {
            if (selectedObject != Selection.activeGameObject) selectedObject = Selection.activeGameObject;

            try
            {
                if (selectedObject.GetComponent<Tilemap>() != null && currentTilemap != selectedObject.GetComponent<Tilemap>())
                {
                    currentTilemap = selectedObject.GetComponent<Tilemap>();
                    currentTilemapCount = GetAmoutOfTilesInTilemap(currentTilemap);
                    previousTilemapCount = GetAmoutOfTilesInTilemap(currentTilemap);
                }
            }
            catch (MissingReferenceException e) { }
            catch (NullReferenceException e) { }

            try { currentTilemap.CompressBounds(); }
            catch { }
        }

        private static int GetAmoutOfTilesInTilemap(Tilemap tilemap)
        {
            int amount = 0;

            foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
            {
                Tile tile = tilemap.GetTile<Tile>(pos);

                if (tile != null)
                {
                    amount += 1;
                }
            }

            return amount;
        }
    }
}
#endif