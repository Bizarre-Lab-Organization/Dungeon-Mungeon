using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DungeonMungeon
{
    public class TransparentWalls : MonoBehaviour
    {
        [SerializeField] private Transform playerObject;
        [SerializeField] private GameObject camera;

        private GameObject currentObject = null;
        private bool isCurrentlyTransparent = false;

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
                    if (currentObject == null)
                    {
                        currentObject = collidedObject;
                        currentObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);

                        isCurrentlyTransparent = true;
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
