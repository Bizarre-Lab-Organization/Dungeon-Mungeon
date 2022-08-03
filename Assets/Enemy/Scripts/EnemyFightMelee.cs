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

        [SerializeField] private float _range = 1;
        [SerializeField] private float _cooldown = 3;
        private float _time = 0;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            Hitting();
        }

        void Hitting()
        {
            _time += Time.deltaTime;
            if(_time >= _cooldown)
            {
                if(IsHit()) Debug.Log("Ko ma biish we");
                _time = 0;
            }
        }

        bool IsHit()
        {
            Debug.Log("aaa");
            float x = _target.transform.position.x - _rb.transform.position.x;
            float y = _target.transform.position.y - _rb.transform.position.y;
            float _distance = Mathf.Sqrt(x*x + y*y);

            if(_distance <= _range)return true;
            return false;
        }
    }
}
