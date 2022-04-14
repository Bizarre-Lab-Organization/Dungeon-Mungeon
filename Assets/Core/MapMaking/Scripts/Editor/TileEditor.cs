using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using UnityEditor.SceneManagement;
using System.Collections.Generic;
using System.Linq;

namespace DungeonMungeon
{
    public class TileEditor : Editor
    {
        [MenuItem("Tilemaps/Compress All")]
        static void CompressAll()
        {
            foreach (Tilemap tilemap in FindObjectsOfType(typeof(Tilemap)) as Tilemap[])
            {
                tilemap.CompressBounds();
            }
        }

        /*[MenuItem("Tilemaps/Get All Tilemaps")]
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
        }*/

        [MenuItem("Tilemaps/Convert To Transparentable")]
        static void ConvertToTransparentable()
        {
            GameObject go = Selection.activeObject as GameObject;

            if (go.GetComponent<Tilemap>() != null && go.GetComponent<TilemapRenderer>() != null)
            {
                if (go.name == "Walls")
                {
                    Tilemap tilemap = go.GetComponent<Tilemap>();
                    Grid grid = go.GetComponentInParent<Grid>();

                    BoundsInt bounds = tilemap.cellBounds;
                    TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
                    
                    for (int x = 0; x < bounds.size.x; x++)
                    {
                        for (int y = 0; y < bounds.size.y; y++)
                        {
                            TileBase tile = allTiles[x + y * bounds.size.x];

                            if (tile != null)
                            {
                                Vector3Int localPlace = new Vector3Int(x, y, (int)tilemap.transform.position.y);
                                Vector3 pos = (grid.GetCellCenterWorld(localPlace) + tilemap.localBounds.center) - tilemap.localBounds.size / 2;

                                bool found = false;

                                foreach (Transform prefab in go.transform)
                                {
                                    if (prefab.transform.position == pos)
                                    {
                                        found = true;
                                        break;
                                    }
                                }

                                if (!found)
                                {
                                    GameObject til = PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/Core/MapMaking/Level_test/Prefabs/Wall Prefab.prefab", typeof(GameObject)) as GameObject) as GameObject;
                                    til.transform.parent = go.transform;
                                    til.transform.position = pos;

                                    tilemap.SetTile(tilemap.WorldToCell(pos), grid.gameObject.GetComponent<GridOptions>().TileToReplaceWith);
                                }
                            }
                        }
                    }
                }
            } else
            {
                Debug.LogWarning("You have to select the Tilemap you want to convert.");
            }
        }

        [MenuItem("Tilemaps/Remove Unsused Transparentable")]
        static void RemoveUnsusedTransparentable()
        {
            GameObject go = Selection.activeObject as GameObject;

            if (go.GetComponent<Tilemap>() != null && go.GetComponent<TilemapRenderer>() != null)
            {
                if (go.name == "Walls")
                {
                    Tilemap tilemap = go.GetComponent<Tilemap>();
                    Grid grid = go.GetComponentInParent<Grid>();

                    BoundsInt bounds = tilemap.cellBounds;
                    TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

                    foreach (Transform prefab in go.transform.Cast<Transform>().ToList()) // help - https://answers.unity.com/questions/605341/why-does-foreach-work-only-12-of-a-time.html
                    {
                        bool found = false;

                        for (int x = 0; x < bounds.size.x; x++)
                        {
                            if (found) continue;

                            for (int y = 0; y < bounds.size.y; y++)
                            {
                                if (found) continue;

                                TileBase tile = allTiles[x + y * bounds.size.x];

                                Vector3Int localPlace = new Vector3Int(x, y, (int)tilemap.transform.position.y);
                                Vector3 pos = (grid.GetCellCenterWorld(localPlace) + tilemap.localBounds.center) - tilemap.localBounds.size / 2;

                                if (prefab.transform.position == pos)
                                {
                                    if (tile == null || tilemap.GetTile(tilemap.WorldToCell(prefab.transform.position)) != grid.gameObject.GetComponent<GridOptions>().TileToReplaceWith)
                                    {
                                        DestroyImmediate(prefab.gameObject);

                                        found = true;
                                    }
                                }
                            }
                        }
                    }
                }
                }
            else
            {
                Debug.LogWarning("You have to select the Tilemap you want to convert.");
            }
        }

        [MenuItem("Tilemaps/Transparentable Rows")]
        static void TransparentableRows()
        {
            GameObject go = Selection.activeObject as GameObject;

            if (go.GetComponent<Tilemap>() != null && go.GetComponent<TilemapRenderer>() != null)
            {
                if (go.name == "Walls")
                {
                    Tilemap tilemap = go.GetComponent<Tilemap>();
                    Grid grid = go.GetComponentInParent<Grid>();

                    BoundsInt bounds = tilemap.cellBounds;
                    TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

                    //clear the list for each
                    foreach (Transform prefab in go.transform.Cast<Transform>().ToList()) // help - https://answers.unity.com/questions/605341/why-does-foreach-work-only-12-of-a-time.html
                    {
                        bool found = false;

                        for (int y = 0; y < bounds.size.y; y++)
                        {
                            if (found) continue;

                            for (int x = 0; x < bounds.size.x; x++)
                            {
                                if (found) continue;

                                TileBase tile = allTiles[x + y * bounds.size.x];

                                Vector3Int localPlace = new Vector3Int(x, y, (int)tilemap.transform.position.y);
                                Vector3 pos = (grid.GetCellCenterWorld(localPlace) + tilemap.localBounds.center) - tilemap.localBounds.size / 2;

                                if (prefab.transform.position == pos)
                                {
                                    if (tile != null)
                                    {
                                        TileBase tileToTheRight = allTiles[(x + 1) + y * bounds.size.x];

                                        Vector3Int localPlaceRight = new Vector3Int((x + 1), y, (int)tilemap.transform.position.y);
                                        Vector3 posRight = (grid.GetCellCenterWorld(localPlaceRight) + tilemap.localBounds.center) - tilemap.localBounds.size / 2;

                                        if (tileToTheRight != null)
                                        {
                                            foreach (Transform prefabRight in go.transform.Cast<Transform>().ToList())
                                            {
                                                if (prefabRight.transform.position == posRight)
                                                {
                                                    //var transforms = Selection.gameObjects.Select(prefabRight => prefabRight.GetComponent<TransparentGroups>().TransparentGroup).ToArray();
                                                    //var so = new SerializedObject(prefabRight);

                                                    TransparentGroups transparentGroup = prefabRight.GetComponent<TransparentGroups>();
                                                    transparentGroup.ClearList();

                                                    //so.FindProperty("transparentGroup") = new List<GameObject>();
                                                    //so.ApplyModifiedProperties();
                                                }
                                            }
                                        }

                                        found = true;
                                    }
                                }
                            }
                        }
                    }

                    //add the right one and give it's self
                    /*foreach (Transform prefab in go.transform.Cast<Transform>().ToList()) // help - https://answers.unity.com/questions/605341/why-does-foreach-work-only-12-of-a-time.html
                    {
                        bool found = false;

                        for (int y = 0; y < bounds.size.y; y++)
                        {
                            if (found) continue;

                            for (int x = 0; x < bounds.size.x; x++)
                            {
                                if (found) continue;

                                TileBase tile = allTiles[x + y * bounds.size.x];

                                Vector3Int localPlace = new Vector3Int(x, y, (int)tilemap.transform.position.y);
                                Vector3 pos = (grid.GetCellCenterWorld(localPlace) + tilemap.localBounds.center) - tilemap.localBounds.size / 2;
                                
                                if (prefab.transform.position == pos)
                                {
                                    if (tile != null)
                                    {
                                        List<GameObject> collectGroup = new List<GameObject>();

                                        collectGroup.Add(prefab.gameObject);



                                        TileBase tileToTheRight = allTiles[(x + 1) + y * bounds.size.x];

                                        Vector3Int localPlaceRight = new Vector3Int((x + 1), y, (int)tilemap.transform.position.y);
                                        Vector3 posRight = (grid.GetCellCenterWorld(localPlaceRight) + tilemap.localBounds.center) - tilemap.localBounds.size / 2;
                                        
                                        if (tileToTheRight != null)
                                        {
                                            foreach (Transform prefabRight in go.transform.Cast<Transform>().ToList())
                                            {
                                                if (prefabRight.transform.position == posRight)
                                                {
                                                    TransparentGroups transparentGroup = prefabRight.GetComponent<TransparentGroups>();
                                                    transparentGroup.AddToGroup(prefab.GetComponent<TransparentGroups>().GetGroup());

                                                    break;
                                                }
                                            }
                                        }

                                        prefab.GetComponent<TransparentGroups>().SetList(collectGroup);

                                        found = true;
                                    }
                                }
                            }
                        }
                    }*/

                    List<GameObject> collectGroup = new List<GameObject>();
                    
                    for (int y = 0; y < bounds.size.y; y++)
                    {
                        List<GameObject> tilesOnRow = new List<GameObject>();

                        for (int x = 0; x < bounds.size.x; x++)
                        {
                            TileBase tile = allTiles[x + y * bounds.size.x];
                            Vector3Int localPlace = new Vector3Int(x, y, (int)tilemap.transform.position.y);
                            Vector3 pos = (grid.GetCellCenterWorld(localPlace) + tilemap.localBounds.center) - tilemap.localBounds.size / 2;

                            if (tile != null)
                            {
                                foreach (Transform prefab in go.transform.Cast<Transform>().ToList())
                                {
                                    if (prefab.transform.position == pos)
                                    {
                                        tilesOnRow.Add(prefab.gameObject);
                                    }
                                }
                            }
                            

                            /*foreach (Transform prefab in go.transform.Cast<Transform>().ToList())
                            {
                                if (prefab.transform.position.y == pos.y)
                                {
                                    if (tile != null)
                                    {
                                        collectGroup.Add(prefab.gameObject);
                                        break;
                                    }
                                }
                            }*/




                                /*TileBase tile = allTiles[x + y * bounds.size.x];
                                Vector3Int localPlace = new Vector3Int(x, y, (int)tilemap.transform.position.y);
                                Vector3 pos = (grid.GetCellCenterWorld(localPlace) + tilemap.localBounds.center) - tilemap.localBounds.size / 2;

                                TileBase tileToTheRight = allTiles[(x + 1) + y * bounds.size.x];
                                Vector3Int localPlaceRight = new Vector3Int((x + 1), y, (int)tilemap.transform.position.y);
                                Vector3 posRight = (grid.GetCellCenterWorld(localPlaceRight) + tilemap.localBounds.center) - tilemap.localBounds.size / 2;
                                GameObject prefabRight = null;

                                foreach (Transform pref in go.transform.Cast<Transform>().ToList())
                                {
                                    if (pref.transform.position == posRight)
                                    {
                                        prefabRight = pref.gameObject;
                                    }
                                }

                                if (prefabRight == null) break;

                                if (prefab.transform.position == pos && prefabRight.transform.position == posRight)
                                {
                                    if (tile != null && tileToTheRight != null)
                                    {
                                        collectGroup.Add(prefabRight);
                                    }
                                }*/
                        }

                        for (int x = 0; x < bounds.size.x; x++)
                        {
                            TileBase tile = allTiles[x + y * bounds.size.x];
                            Vector3Int localPlace = new Vector3Int(x, y, (int)tilemap.transform.position.y);
                            Vector3 pos = (grid.GetCellCenterWorld(localPlace) + tilemap.localBounds.center) - tilemap.localBounds.size / 2;

                            foreach (Transform prefab in go.transform.Cast<Transform>().ToList())
                            {
                                if (prefab.transform.position == pos)
                                {
                                    if (tile != null)
                                    {
                                        //prefab.GetComponent<TransparentGroups>().SetList(tilesOnRow);
                                        prefab.GetComponent<TransparentGroups>().TransparentGroup = tilesOnRow;
                                        break;
                                    }
                                }
                            }
                        }

                        Debug.Log("On row " + (y + 1) + " there are " + tilesOnRow.Count + " tiles");
                    }
                }
            }
            else
            {
                Debug.LogWarning("You have to select the Tilemap you want to convert.");
            }

            /*if (GUI.changed)
            {
                EditorUtility.SetDirty(castedTarget);
                EditorSceneManager.MarkSceneDirty(castedTarget.gameObject.scene);
            }*/
        }

        private const string automaticTransparentable = "Automatic Transparentable";
        private static bool enabledAutomaticTransparentable = false;

        static void AutomaticTransparentable()
        {
            enabledAutomaticTransparentable = EditorPrefs.GetBool(automaticTransparentable, true);
        }

        [MenuItem("Tilemaps/" + automaticTransparentable)]
        private static void ToggleAutomaticTransparentable()
        {
            enabledAutomaticTransparentable = !enabledAutomaticTransparentable;
            EditorPrefs.SetBool(automaticTransparentable, enabledAutomaticTransparentable);
        }

        [MenuItem("Tilemaps/" + automaticTransparentable, true)]
        private static bool ToggleAutomaticTransparentableActionValidate()
        {
            Menu.SetChecked("Tilemaps/" + automaticTransparentable, enabledAutomaticTransparentable);
            return true;
        }

        private void OnEnable()
        {
            AutomaticTransparentable();
        }
    }
}

// list of block of transparents