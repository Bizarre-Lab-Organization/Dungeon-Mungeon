using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonMungeon
{
    public class Player : MonoBehaviour
    {
        private float _speed = 2400;
        private Rigidbody2D _rb;
        void Awake()
        {
            _rb = gameObject.GetComponent<Rigidbody2D>();
        
        }

        void Update()
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputY = Input.GetAxisRaw("Vertical");

            _rb.velocity = new Vector2(inputX * _speed * Time.deltaTime, inputY * _speed * Time.deltaTime).normalized;

            
        
        }
    }
}
