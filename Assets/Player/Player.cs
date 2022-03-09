using UnityEngine;

namespace DungeonMungeon
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Camera _camera;
        private Rigidbody2D _rb;

        void Awake()
        {
            _rb = gameObject.GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputY = Input.GetAxisRaw("Vertical");

            if (inputX != 0 || inputY != 0)
            {
                if (inputX != 0 && inputY != 0)
                {
                    inputX *= 1f;
                    inputY *= 1f;
                }

                _rb.velocity = new Vector2(inputX * _speed, inputY * _speed);
                _camera.transform.position = new Vector3(_rb.position.x, _rb.position.y, -10);
            }
            else
            {
                _rb.velocity = new Vector2(0, 0);
            }
        }
    }
}
