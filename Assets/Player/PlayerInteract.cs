using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonMungeon
{
    public class PlayerInteract : interactableOptions
    {
        public float interactRange;
        Transform bestTarget = new GameObject().transform;
        Transform prevBestTarget = new GameObject().transform;
        private LayerMask interactableLayers;
        [SerializeField] Text interactText;
        public Material outliners;
        public Material defaultSprite;
        private Type options;
        void Awake()
        {
            interactableLayers = LayerMask.GetMask("Interactables");
        }

        void Update()
        {
            Interact_prompt();
        }
        private void Interact_prompt()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, interactRange, interactableLayers);

            prevBestTarget = bestTarget;
            bestTarget = null;
            float closestDistanceSqrt = Mathf.Infinity;
            Vector3 playerPos = transform.position;

            foreach (Collider2D target in colliders)
            {
                Vector3 direction = target.transform.position - playerPos;
                float distToTarget = direction.sqrMagnitude;
                if (distToTarget < closestDistanceSqrt)
                {
                    closestDistanceSqrt = distToTarget;
                    bestTarget = target.transform;
                }

            }
            if (bestTarget == null && prevBestTarget == null) return;


            if (!prevBestTarget.Equals(bestTarget))
            {
                prevBestTarget.GetComponent<SpriteRenderer>().material = defaultSprite;
            }
            if (bestTarget != null)
            {
                interactText.enabled = true;
                options = bestTarget.GetComponent<interactableOptions>()._type;
                if (options.Equals(Type.Button))
                {
                    interactText.text = "Push Button";
                }
                if (options.Equals(Type.Door))
                {
                    interactText.text = "Open/Close Door";
                }
                if (options.Equals(Type.Item))
                {
                    interactText.text = "Pickup " + bestTarget.name;
                }
                //Debug.Log(options);
                //interactText.text = "Interact with " + bestTarget.name;
                Debug.Log(bestTarget.name);
                bestTarget.GetComponent<SpriteRenderer>().material = outliners;
            }
            else
            {
                interactText.enabled = false;
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(gameObject.transform.position, interactRange);
        }
    }
}
