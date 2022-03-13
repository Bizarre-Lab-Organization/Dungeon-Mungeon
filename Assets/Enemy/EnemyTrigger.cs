using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonMungeon
{
    public class EnemyTrigger : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision) {
            if(collision.gameObject.tag == "Player"){
                this.GetComponent<EnemyAI>().enabled = true;
            }
        }
    }
}
