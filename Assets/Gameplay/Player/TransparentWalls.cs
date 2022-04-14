using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

namespace DungeonMungeon
{
    public class TransparentWalls : MonoBehaviour
    {
        [SerializeField] private Transform playerObject;
        [SerializeField] private GameObject camera;

        private GameObject currentObject = null;

        private void Start()
        {
            //Physics2D.IgnoreCollision(gameObject.GetComponent<EdgeCollider2D>(), collision.gameObject.GetComponent<TilemapCollider2D>());
            Physics2D.IgnoreLayerCollision(0, 9);
        }

        void Update()
        {
            Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(playerObject.position);
            RaycastHit2D hitinfo = Physics2D.Raycast(camera.transform.position, rayOrigin, 0, LayerMask.GetMask("transparentwallplayercheck"));

            if (hitinfo.collider != null)
            {
                GameObject collidedObject = hitinfo.collider.gameObject;
                    
                if (collidedObject.layer == 9)
                {
                    if (currentObject == null && !asd(collidedObject).Contains(collidedObject))
                    {
                        currentObject = collidedObject;
                        currentObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);

                        foreach (GameObject prefab in asd(currentObject))
                        {
                            currentObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                        }

                    } else if (!collidedObject.Equals(currentObject))
                    {
                        currentObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                        currentObject = collidedObject;
                        currentObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                    }
                } else
                {
                    if (currentObject == null) return;
                    currentObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    currentObject = null;
                }
            } else
            {
                if (currentObject == null) return;
                currentObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                currentObject = null;
            }
        }

        private List<GameObject> asd(GameObject go)
        {
            List<GameObject> coll = new List<GameObject>();

            Tilemap tilemap = go.transform.parent.GetComponent<Tilemap>();
            Grid grid = go.transform.parent.parent.GetComponentInParent<Grid>();

            BoundsInt bounds = tilemap.cellBounds;
            TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

            List<GameObject> goTiles = new List<GameObject>();

            for (int y = 0; y < bounds.size.y; y++)
            {
                for (int x = 0; x < bounds.size.x; x++)
                {
                    TileBase tile = allTiles[x + y * bounds.size.x];
                    Vector3Int localPlace = new Vector3Int(x, y, (int)tilemap.transform.position.y);
                    Vector3 pos = (grid.GetCellCenterWorld(localPlace) + tilemap.localBounds.center) - tilemap.localBounds.size / 2;

                    if (tile != null)
                    {
                        foreach (Transform prefab in go.transform.parent.transform.Cast<Transform>().ToList())
                        {
                            if (prefab.transform.position == pos)
                            {
                                goTiles.Add(prefab.gameObject);
                            }
                        }
                    }
                }
            }

            foreach (GameObject prefab in goTiles)
            {
                if (prefab.transform.position.y == go.transform.position.y)
                {
                    coll.Add(prefab);
                }
            }

            return coll;
        }

        /*private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == 7)
            {
                Debug.Log(2);
                Physics2D.IgnoreCollision(gameObject.GetComponent<EdgeCollider2D>(), collision.gameObject.GetComponent<TilemapCollider2D>());

                SpriteRenderer spriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                }
            }
        }*/
    }
}
