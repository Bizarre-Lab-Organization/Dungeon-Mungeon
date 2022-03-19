using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonMungeon
{
    public class EnemyRange : MonoBehaviour
    {
        private EnemyManager enemyManager;

        [SerializeField] private float _rangeOn = 5, _rangeOff = 10;
        private float _distance, x, y;
        private Rigidbody2D _rb;
        [SerializeField] private Rigidbody2D _player;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();

            enemyManager = gameObject.GetComponent<EnemyManager>();

            _player = enemyManager.Target.gameObject.GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            x = Mathf.Abs(_player.transform.position.x - _rb.transform.position.x);
            y = Mathf.Abs(_player.transform.position.y - _rb.transform.position.y);
            _distance = Mathf.Sqrt(x*x + y+y);

            Trigger();
            Untrigger();
        }

        void Trigger(){
            if(_distance <= _rangeOn){
                this.GetComponent<EnemyAI>().enabled = true;
            }
        }

        void Untrigger(){
            if(_distance >= _rangeOff){
                this.GetComponent<EnemyAI>().enabled = false;
                _rb.velocity = Vector2.zero;
            }
        }
    }
}
