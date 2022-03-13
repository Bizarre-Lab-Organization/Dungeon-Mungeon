using UnityEngine;

namespace DungeonMungeon
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Camera _camera;
        [SerializeField] private float health = 100;
        [SerializeField] private Transform attackPoint;
        [SerializeField] private float range = 0.2f;
        [SerializeField] private LayerMask enemyLayers;

        private Rigidbody2D _rb;
        private Vector2 _moveDir;

        

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();

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
                Debug.Log("Hit" + enemy.name);
            }
        }
        private void OnDrawGizmos()
        {
            if(attackPoint == null)
            {
                return;
            }
            Gizmos.DrawWireSphere(attackPoint.position, range);
            
        }     

    }
}