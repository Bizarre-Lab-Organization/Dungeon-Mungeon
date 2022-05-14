using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DungeonMungeon
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private Vector2 _moveDir;

        [SerializeField]
        private Camera _camera;
        [SerializeField]
        private float _speed;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Move(InputAction.CallbackContext ctx)
        {
            _moveDir = ctx.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            _rigidbody2D.velocity = _moveDir * _speed * Time.deltaTime;
            _camera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
    }
}
