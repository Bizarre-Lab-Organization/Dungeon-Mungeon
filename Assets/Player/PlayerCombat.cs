using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonMungeon
{
    public class PlayerCombat : MonoBehaviour
    {
        public static event EventManager.SetSpeed SetSpeed;

        private LayerMask enemyLayers;
        public Transform attackPoint;
        public float range;
        [SerializeField] private float attackCooldown;
        private float timeTillAttack;

        [SerializeField] private float knockbackStrenght;
        [SerializeField] private float knockTime;
        void Awake()
        {
            enemyLayers = LayerMask.GetMask("Enemy");
            timeTillAttack = attackCooldown;
        }

        void Update()
        {
            //Debug.Log(timeTillAttack);
            if(timeTillAttack > 0) 
            {
                timeTillAttack -= Time.deltaTime;
            }
            else 
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    SetSpeed(0); 
                    timeTillAttack = attackCooldown;
                    Attack();
                }
            }
            
        }
        private void Attack()
        {
            Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, range, enemyLayers);

            foreach (Collider2D enemy in enemiesHit)
            {
                Debug.Log("Hit " + enemy.name);
                // Destroy(enemy.gameObject);
                AddKnockback(knockbackStrenght, enemy.gameObject);
            }
        }

        private void AddKnockback(float strenght, GameObject target)
        {
            Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                if (!rb.freezeRotation) rb.freezeRotation = true;
                rb.isKinematic = false;
                Vector2 difference = target.transform.position - transform.position;
                difference = difference.normalized * strenght;
                rb.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(KnockCo(rb));
                 
            }
        }

        private IEnumerator KnockCo(Rigidbody2D rb)
        {
            if(rb != null)
            {
                yield return new WaitForSeconds(knockTime);
                rb.velocity = Vector2.zero;
                rb.isKinematic = true;
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
