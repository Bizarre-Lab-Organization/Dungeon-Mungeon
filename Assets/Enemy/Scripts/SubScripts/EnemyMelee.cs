using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace DungeonMungeon
{
    public class EnemyMelee : MonoBehaviour
    {
        private EnemyManager enemyManager;

        [SerializeField] private Transform _target;

        [SerializeField] private float _speed;
        private float _nextWaypointDistance = 3f;

        Path path;
        int currentWaypoint = 0;
        bool reachedEndOfPath = false;

        private Seeker _seeker;
        private Rigidbody2D _rb; 

        private void Awake()
        {
            enemyManager = gameObject.GetComponent<EnemyManager>();

            _speed = enemyManager.Speed;
            _target = enemyManager.Target;
        }

        void Start()
        {
            _seeker = GetComponent<Seeker>();
            _rb = GetComponent<Rigidbody2D>();

            InvokeRepeating("UpdatePath", 0f, 0.5f);
            _seeker.StartPath(_rb.position, _target.position, OnPathComplete);
        }

        void FixedUpdate()
        {
            if (path == null) return;

            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }else
            {
                reachedEndOfPath = false;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - _rb.position).normalized;
            _rb.velocity = direction * _speed * Time.deltaTime;

            float distance = Vector2.Distance(_rb.position, path.vectorPath[currentWaypoint]);

            if (distance < _nextWaypointDistance)
            {
                currentWaypoint++;
            }
        }

        void OnPathComplete(Path p)
        {
            if(!p.error)
            {
                path = p;
                currentWaypoint = 0;
            }
        }

        void UpdatePath(){
            if (_seeker.IsDone()) _seeker.StartPath(_rb.position, _target.position, OnPathComplete);
        }
    }
}
