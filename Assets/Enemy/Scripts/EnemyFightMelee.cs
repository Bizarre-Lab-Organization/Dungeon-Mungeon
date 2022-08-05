using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonMungeon
{
    public class EnemyFightMelee : MonoBehaviour
    {
        private EnemyManager enemyManager;
        [SerializeField] private Transform _target;
        private Rigidbody2D _rb;

        [SerializeField] private float _attackRange = 1;
        [SerializeField] private float _attackCooldown = 3;

        
        private float _AtTime = 0;


        float x, y, _distance;

        void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
            _rb = GetComponent<Rigidbody2D>();
            _target = enemyManager.Target;
        }

        void FixedUpdate()
        {
            x = _target.transform.position.x - _rb.transform.position.x;
            y = _target.transform.position.y - _rb.transform.position.y;

            _distance = Mathf.Sqrt(x*x + y*y);

            Hitting();
        }

        void Hitting()
        {
            _AtTime += Time.deltaTime;
            if(_AtTime >= _attackCooldown)
            {
                if(IsHit()) Debug.Log("Ko ma biish we");
                _AtTime = 0;
            }
            
        }

        bool IsHit()
        {
            Debug.Log("Vragat se opitva da te udari");
            
            if(_distance <= _attackRange)return true;
            return false;
        }
    }
}
