using UnityEngine;

namespace DungeonMungeon
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Camera _camera;
        private Rigidbody2D _rb;
        private Vector2 _moveDir;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _camera.orthographicSize = transform.localScale.z * 4;
            _speed = transform.localScale.z * 150;
        }

        void Update()
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputY = Input.GetAxisRaw("Vertical");

            _moveDir = new Vector2(inputX, inputY).normalized;
            _camera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }

        private void FixedUpdate()
        {
            _rb.velocity = _moveDir * _speed * Time.deltaTime;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == 6)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}