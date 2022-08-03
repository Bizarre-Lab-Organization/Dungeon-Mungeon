using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonMungeon
{
    public class EnemyHealth : MonoBehaviour
    {
        private HealthSystem enemyHP;
        [SerializeField] private int maxEnemyHP;

        void Awake()
        {
            enemyHP = new HealthSystem(maxEnemyHP);
        }

        void Update()
        {
        
        }

        private void OnEnemyHit(int damage)
        {
            enemyHP.TakeDamage(damage);
            if(enemyHP.GetHealth() == 0)
            {
                Die();
            }
        }

        private void OnEnemyHeal(int heal)
        {
            if(enemyHP.GetHealth() < maxEnemyHP)
            {
                enemyHP.Heal(heal);
            }
        }

        private void Die()
        {
            if(gameObject != null)
            {
                Destroy(gameObject);
            }
        }
    }
}
