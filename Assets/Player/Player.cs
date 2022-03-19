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
    }

    /*public static class StaticPlayer
    {
        [HideInInspector] public static GameObject playerObject;
    }*/
}