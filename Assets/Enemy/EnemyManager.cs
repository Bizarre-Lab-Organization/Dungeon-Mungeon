using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonMungeon
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] public bool prassive; // if not passive - hostile
        [SerializeField] public bool ranged; // if not ranged - melee

        //Ranged:
        [HideInInspector] public int range;

        /*void Awake()
        {
            range = EnemyEditor.
        }*/

        private void Start()
        {
            Debug.Log(range);
        }
    }
}
