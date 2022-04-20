using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DungeonMungeon
{
    public class RemoveInteractablesTileSprites : MonoBehaviour
    {
        void Start()
        {
            TilemapRenderer renderer;
            gameObject.TryGetComponent<TilemapRenderer>(out renderer);

            if (renderer != null)
            {
                Destroy(renderer);
            }
        }
    }
}
