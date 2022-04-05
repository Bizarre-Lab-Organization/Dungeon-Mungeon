using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace DungeonMungeon
{
    public class PlayerManager : MonoBehaviour
    {
        public float _speed;
        public float health;
        public float range;
        public float interactRange;
        [SerializeField] GameObject promp;
        bool isCreated = false;
        //private Collider2D[] interactables = { };
        //private Collider2D[] interactableslist = { };

        private List<Collider2D> interactables = new List<Collider2D>();
        private List<Collider2D> interactableslist = new List<Collider2D>();
        Transform bestTarget = new GameObject().transform;
        Transform prevBestTarget = new GameObject().transform;

        public Transform attackPoint;
        public Camera _camera;
        public LayerMask enemyLayers;
        private Rigidbody2D _rb;
        private Vector2 _moveDir;     
        private LayerMask interactableLayers;
        [SerializeField] Text interactText;
        public Material outliners;
        public Material defaultSprite;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();

            interactableLayers = LayerMask.GetMask("Interactables");
            
        }

        void Update()
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputY = Input.GetAxisRaw("Vertical");

            _moveDir = new Vector2(inputX, inputY).normalized;
            _camera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
            }

            Interact_prompt();
        }

        private void FixedUpdate()
        {
            _rb.velocity = _moveDir * _speed * Time.deltaTime;
        }

        private void Attack()
        {
            Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, range, enemyLayers);

            foreach(Collider2D enemy in enemiesHit)
            {
                Debug.Log("Hit " + enemy.name);
                Destroy(enemy.gameObject);
            }
        }

        private void Interact_prompt()
        {
            interactables.Clear();

            Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, interactRange, interactableLayers);

            prevBestTarget = bestTarget;
            bestTarget = null;
            //Transform current = null;
            float closestDistanceSqrt = Mathf.Infinity;
            Vector3 playerPos = transform.position;

           /* if(colliders.Length == 0)
            {
                current = null;
            }

            else*/

            
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
                interactText.text = "Interact with " + bestTarget.name;
                Debug.Log(bestTarget.name);
                bestTarget.GetComponent<SpriteRenderer>().material = outliners;    
            }
            else 
            {
                //prevBestTarget.GetComponent<SpriteRenderer>().material = defaultSprite;
                //prevBestTarget = null;
                interactText.enabled = false;
            }

            
            
            /*if (current != null)
            {
                Debug.Log(1);
                {
                    if (!current.Equals(bestTarget))
                    {
                        Debug.Log(3);
                        bestTarget = current;
                    }
                    else Debug.Log(4);
                } 
            }
            else
            {
                Debug.Log(2);
                Destroy(prevBestTarget.transform.Find("tek").gameObject);
                bestTarget = null;
            }

            try
            {
                if (!bestTarget.Equals(prevBestTarget))
                {
                    Debug.Log(prevBestTarget + " " + bestTarget);
                    Destroy(prevBestTarget.transform.Find("tek").gameObject);
                    
                }
            }
            catch { };

            if (bestTarget != null) prevBestTarget = bestTarget;

            if (!prevBestTarget.Find("tek"))
            {
                GameObject prop = Instantiate(promp, prevBestTarget.transform.position + new Vector3(0, 1.5f), new Quaternion(), prevBestTarget.transform);
                prop.name = "tek";
            }*/
        }

        private void OnDrawGizmos()
        {
            if(attackPoint == null)
            {
                return;
            }
            Gizmos.DrawWireSphere(attackPoint.position, range);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(gameObject.transform.position, interactRange);
        }
    }
}