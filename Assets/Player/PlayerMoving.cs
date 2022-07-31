using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonMungeon
{
    public class PlayerMoving : MonoBehaviour
    {
        private Rigidbody2D _rb;
        [SerializeField] private float origSpeed;
        private float _speed;
        public Camera _camera;
        private Vector2 _moveDir;
        [SerializeField] private float moveCooldown;
        private float timeTillMove;


        void Awake()
        {
            _speed = origSpeed;
            _rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            PlayerCombat.SetSpeed += StopInPlace;
        }

        private void OnDisable()
        {
            PlayerCombat.SetSpeed -= StopInPlace;
        }

        void Update()
        {
            if (timeTillMove > 0)
            {
                timeTillMove -= Time.deltaTime;
            }
            else
            {
                if (_speed != origSpeed) _speed = origSpeed;
            }
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputY = Input.GetAxisRaw("Vertical");

            _moveDir = new Vector2(inputX, inputY).normalized;
            _camera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
        void FixedUpdate()
        {
            _rb.velocity = _moveDir * _speed * Time.deltaTime;
        }

        private void StopInPlace(float speed)
        {
            _speed = speed;
            timeTillMove = moveCooldown;

        }
    }
}
