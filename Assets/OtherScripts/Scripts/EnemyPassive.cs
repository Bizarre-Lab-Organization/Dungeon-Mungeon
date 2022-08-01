using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonMungeon
{
    public class EnemyPassive : MonoBehaviour
    {
        private EnemyManager enemyManager;
        void Awake (){
            enemyManager = GetComponent<EnemyManager>();
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if(collision.gameObject.tag == "Player"){
                if (!enemyManager.Ranged)
                {
                    this.GetComponent<EnemyWalking>().enabled = true;
                }
                else
                {
                    this.GetComponent<EnemyRanged>().enabled = true;
                }
            }
        }
    }
}
