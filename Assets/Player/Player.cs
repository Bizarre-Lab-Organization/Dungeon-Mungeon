using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonMungeon
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _speed = 4400;
        private Rigidbody2D _rb;
        void Awake()
        {
            _rb = gameObject.GetComponent<Rigidbody2D>();
        
        }

        void Update()
        {
            /* float inputX = Input.GetAxisRaw("Horizontal");
             float inputY = Input.GetAxisRaw("Vertical");

             var movement = new Vector2(inputX, inputY).normalized;

             _rb.velocity = movement * _speed * Time.deltaTime;
             */
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
            }
            else
            {
                _rb.velocity = new Vector2(0, 0);
            }

        }
    }
}
