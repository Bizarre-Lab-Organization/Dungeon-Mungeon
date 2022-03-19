using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonMungeon
{
    public class EnemyManager : MonoBehaviour // sub scripts fetch from here
    {
        [Header("Type")]
        [SerializeField] private bool _passive; // if not passive - hostile

        [SerializeField] private bool _ranged; // if not ranged - melee
        private int _range;

        [Header("Common")]
        [SerializeField] private float _speed;
        [SerializeField] private Transform _target;

        public bool Passive
        {
            get { return _passive; }
            set { _passive = value; }
        }

        public bool Ranged
        {
            get { return _ranged; }
            set { _ranged = value; }
        }

        public int Range
        {
            get { return _range; }
            set { _range = value; }
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

        private void Start()
        {
            Debug.Log(_range);
        }
    }
}
