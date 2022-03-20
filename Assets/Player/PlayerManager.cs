using UnityEngine;
using System.Collections;
using System;


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
        private Collider2D[] interactables = { };
        private Collider2D[] interactableslist = { };

        public Transform attackPoint;
        public Camera _camera;
        public LayerMask enemyLayers;
        private Rigidbody2D _rb;
        private Vector2 _moveDir;     
        private LayerMask interactableLayers;


        

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
            interactables = Physics2D.OverlapCircleAll(gameObject.transform.position, interactRange, interactableLayers);
            //Debug.Log(interactableslist.Length);
            // Debug.Log(interactables.Length + " " + interactableslist.Length);
            foreach (Collider2D manja in interactableslist)
            {
                
                    Debug.Log(manja);
                    

            }
            foreach (Collider2D manja in interactables)
            {
                Debug.Log(manja);
            }
            Debug.Log("---------------------------------------------");
            foreach (Collider2D manja in interactableslist)
            {
                foreach(Collider2D baax in interactables)
                {
                    if (Array.Find(interactables, element => manja))
                    {
                        
                    }
                }
                // Debug.Log(manja);
                //Debug.Log(manja in int);
                /*int edikvosi = Array.IndexOf(interactables, manja);
                Debug.Log(edikvosi);
                if (edikvosi >= -1) 
                {
                    Debug.Log("truwe");
                    Destroy(manja.transform.Find("tek").gameObject);
                }
                else Debug.Log(false);*/
               /* if (Array.IndexOf(interactables, manja)) //bruhmomento JNE!
                {
                    Debug.Log("manja");
                    Destroy(manja.transform.Find("tek").gameObject);
                }*/
                
            }


            interactableslist = interactables; 
            foreach (Collider2D item in interactableslist)
            {
                if (item.transform.Find("tek")) return;
                Debug.Log("biden");
                GameObject prop = Instantiate(promp, item.transform.position + new Vector3(0, 1.5f), new Quaternion(), item.transform);
                prop.name = "tek"; 
            }

            //Debug.Log(interactables.Length);
           // Debug.Log(interactables);
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