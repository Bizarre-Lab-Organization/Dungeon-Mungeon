using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace DungeonMungeon
{
    public class EnemyRanged : MonoBehaviour
    {
        private EnemyManager enemyManager;

        [SerializeField] private Transform _target;

        [SerializeField] private float _speed = 300f;
        [SerializeField] private float _range;
        [SerializeField] private float _nextWaypointDistance = 3f;

        Path path;
        int currentWaypoint = 0;
        bool reachedEndOfPath = false;

        private Seeker _seeker;
        private Rigidbody2D _rb; 

        float d = 0;
        Vector3 r = new Vector3(0, 0, 0);

        private void Awake()
        {
            enemyManager = gameObject.GetComponent<EnemyManager>();
            _range = enemyManager.RangeToStayAt;
            _speed = enemyManager.Speed;
            _target = enemyManager.Target;
        }

        void Start()
        {
            _seeker = GetComponent<Seeker>();
            _rb = GetComponent<Rigidbody2D>();

            InvokeRepeating("UpdatePath", 0f, 0.5f);

            Change();
            _seeker.StartPath(_rb.position, r, OnPathComplete);
        }


        void FixedUpdate()
        {
            Change();


            if (path == null) return;

            if(currentWaypoint >= path.vectorPath.Count)
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


            if (d > _range - 0.5 && d < _range + 0.5){
                _rb.velocity = Vector2.zero;
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
            if (_seeker.IsDone()) _seeker.StartPath(_rb.position, r, OnPathComplete);
        }


        void Change(){
            float x = _target.position.x - _rb.position.x;
            float y = _target.position.y - _rb.position.y;

            d = Mathf.Sqrt(x*x + y*y);

            if (d > _range){
                r = _target.position;
            }
            else
            {
                r = new Vector3(_rb.position.x - x, _rb.position.y - y, 0);
            }

        }
    }
}
