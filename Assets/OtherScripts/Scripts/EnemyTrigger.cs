using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonMungeon
{
    public class EnemyTrigger : MonoBehaviour
    {
        private EnemyManager enemyManager;

        [SerializeField] private float _rangeOn, _rangeOff;
        private float _distance, x, y;
        private Rigidbody2D _rb;
        [SerializeField] private Rigidbody2D _player;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();

            enemyManager = gameObject.GetComponent<EnemyManager>();

            _player = enemyManager.Target.gameObject.GetComponent<Rigidbody2D>();
            _rangeOn = enemyManager.RangeStart;
            _rangeOff = enemyManager.RangeEnd;
        }

        void Update()
        {
            x = _player.transform.position.x - _rb.transform.position.x;
            y = _player.transform.position.y - _rb.transform.position.y;
            _distance = Mathf.Sqrt(x*x + y*y);

            Trigger();
            Untrigger();
        }

        void Trigger(){
            if(_distance <= _rangeOn){
                if (!enemyManager.Ranged)
                {
                    this.GetComponent<EnemyWalking>().enabled = true;
                    
                } else
                {
                    this.GetComponent<EnemyRanged>().enabled = true;
                    this.GetComponent<EnemyFightRanged>().enabled = true;
                }
            }
        }

        void Untrigger(){
            if(_distance >= _rangeOff){
                if (!enemyManager.Ranged)
                {
                    this.GetComponent<EnemyWalking>().enabled = false;
                    
                } else
                {
                    this.GetComponent<EnemyRanged>().enabled = false;
                    this.GetComponent<EnemyFightRanged>().enabled = false;
                }
                _rb.velocity = Vector2.zero;
            }
        }
    }
}
