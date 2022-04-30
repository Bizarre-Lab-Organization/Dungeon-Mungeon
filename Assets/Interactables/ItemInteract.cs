using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonMungeon
{
    public class ItemInteract : MonoBehaviour
    {
        private void OnEnable()
        {
            InteractableOptions.Execute += PickUp;
        }
        private void OnDisable()
        {
            InteractableOptions.Execute -= PickUp;
        }
        private void PickUp(Transform target)
        {
            if (target.gameObject == gameObject)
            {
                Destroy(gameObject);
            }
        }
    }
}
