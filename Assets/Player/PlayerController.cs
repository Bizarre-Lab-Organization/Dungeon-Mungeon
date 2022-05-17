using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DungeonMungeon
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent (typeof(PlayerInput))]
    public class PlayerController : MonoBehaviour
    {
        private PlayerInputActions _playerInput;
        private Rigidbody2D _rigidbody2D;
        private Vector2 _moveDir;

        [SerializeField]
        private Camera _camera;
        [SerializeField]
        private float _speed;

        private void Awake()
        {
            _playerInput = new PlayerInputActions();
            _playerInput.Enable();
            _playerInput.Player.Movement.performed += OnMovementPerformed;
            _playerInput.Player.Movement.canceled += OnMovementCanceled;
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnMovementPerformed(InputAction.CallbackContext ctx)
        {
            _moveDir = ctx.ReadValue<Vector2>();
        }

        private void OnMovementCanceled(InputAction.CallbackContext ctx)
        {
            _moveDir = Vector2.zero;
        }

        private void FixedUpdate()
        {
            _rigidbody2D.velocity = _moveDir * _speed * Time.deltaTime;
        }

        private void Update()
        {
            _camera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
    }
}
