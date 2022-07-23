using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonMungeon
{
    public class BulletScript : MonoBehaviour
    {
        public Transform _target;
        private Rigidbody2D _bullet;
        public float _timeInAir;

        [SerializeField] private float _speed;

        Vector2 destination;
        float _time = 0;
        void Awake(){
            _bullet = GetComponent<Rigidbody2D>();
        }
        
        void Start(){
            destination = new Vector2(_target.position.x - _bullet.transform.position.x, _target.position.y - _bullet.transform.position.y).normalized;
        }

        void FixedUpdate(){
            _bullet.velocity = destination * _speed * Time.deltaTime;
            if(_time >= _timeInAir){
                Destroy(gameObject);
                _time = 0;
            }
            _time += Time.deltaTime;
            
        }

        private void OnCollisionEnter2D(Collision2D collision){
            if(collision.gameObject.tag == "Player"){
                Debug.Log("oh");
                
            }
            Destroy(gameObject);
        }
    }
}
