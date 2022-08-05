using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonMungeon
{
    public class EnemyFightRanged : MonoBehaviour
    {
        private EnemyManager enemyManager;
        [SerializeField] private GameObject _bullet;
        [SerializeField] private float _speed;
        [SerializeField] private float _cooldown;
        [SerializeField] private float _timeInAir;

        [SerializeField] private Transform _target;
        private Rigidbody2D _rb, _rbBullet;

        Vector2 destination;

        private float _timer = 0f;

        void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
            _rb = GetComponent<Rigidbody2D>();
            _target = enemyManager.Target;
        }

        void FixedUpdate()
        {
            if(_timer >= _cooldown){
                Shoot();
                _timer = 0f;
            }

            _timer += Time.deltaTime;
        }


        void Shoot(){
            GameObject bullet = Instantiate(_bullet) as GameObject;
            bullet.SetActive(true);
              
            bullet.GetComponent<BulletScript>()._target = _target;
            bullet.GetComponent<BulletScript>()._timeInAir = _timeInAir;

            destination = new Vector2(_target.position.x - gameObject.transform.position.x, _target.position.y - gameObject.transform.position.y).normalized;
            Vector3 d = destination;
            bullet.transform.position = gameObject.transform.position + d * 0.7f;
        }
        
    }
}
