using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonMungeon
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private Rigidbody2D _rb;
        private Vector2 _moveDir;
        [SerializeField] private Rigidbody2D _player;
        private bool _colPlayer = false;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            _moveDir = new Vector2(_player.transform.position.x - _rb.transform.position.x, _player.transform.position.y - _rb.transform.position.y).normalized;
        }

        private void FixedUpdate()
        {
            if(!_colPlayer){
                _rb.velocity = _moveDir * _speed * Time.deltaTime;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if(collision.gameObject.tag == "Player"){
                Debug.Log("Udariha me");
                _colPlayer = true;
               // _player.AddForce(new Vector2(0, 4000)); //= _moveDir * _speed * 200 * Time.deltaTime;
            }
        }

        private void OnCollisionExit2D(Collision2D collision) {
            if(collision.gameObject.tag == "Player"){
                _colPlayer = false;
            }
        }
    }
}
