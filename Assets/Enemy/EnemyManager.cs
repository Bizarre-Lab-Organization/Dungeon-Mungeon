using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonMungeon
{
    public class EnemyManager : MonoBehaviour
    {
        [Header("Type")]
        [SerializeField] private bool _passive; // if not passive - hostile
        [SerializeField] [HideInInspector] private float _rangeStart; // when hostile
        [SerializeField] [HideInInspector] private float _rangeEnd;
        
        [SerializeField] private bool _ranged; // if not ranged - melee
        [SerializeField] [HideInInspector] private float _rangeToStayAt;

        [Header("Common")]
        [SerializeField] private float _speed;
        [SerializeField] private Transform _target;

        public bool Passive
        {
            get { return _passive; }
            set { _passive = value; }
        }

        public float RangeStart
        {
            get { return _rangeStart; }
            set { _rangeStart = value; }
        }

        public float RangeEnd
        {
            get { return _rangeEnd; }
            set { _rangeEnd = value; }
        }

        public bool Ranged
        {
            get { return _ranged; }
            set { _ranged = value; }
        }

        public float RangeToStayAt
        {
            get { return _rangeToStayAt; }
            set { _rangeToStayAt = value; }
        }

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Transform Target
        {
            get { return _target; }
            set { _target = value; }
        }
    }
}
