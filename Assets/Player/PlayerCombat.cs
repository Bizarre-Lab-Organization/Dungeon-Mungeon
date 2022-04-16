using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonMungeon
{
    public class PlayerCombat : MonoBehaviour
    {
        private LayerMask enemyLayers;
        public Transform attackPoint;
        public float range;
        public float health;
        void Awake()
        {
            enemyLayers = LayerMask.GetMask("Enemy");
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
            }
        }
        private void Attack()
        {
            Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, range, enemyLayers);

            foreach (Collider2D enemy in enemiesHit)
            {
                Debug.Log("Hit " + enemy.name);
                Destroy(enemy.gameObject);
            }
        }
        private void OnDrawGizmos()
        {
            if (attackPoint == null)
            {
                return;
            }
            Gizmos.DrawWireSphere(attackPoint.position, range);
        }
    }
}
