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
        //[SerializeField] private float knockTime;
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
                //StartCoroutine(KnockCo(rb));
                StartCoroutine(SlowOverTime(rb));
                 
            }
        }

        /*private IEnumerator KnockCo(Rigidbody2D rb)
        {
            if(rb != null)
            {
                yield return new WaitForSeconds(knockTime);
                rb.velocity = Vector2.zero;
                rb.isKinematic = true;
            }
        }*/
        private IEnumerator SlowOverTime(Rigidbody2D rb)
        {
            /*velo.x = Mathf.Abs(velo.x);
            velo.y = Mathf.Abs(velo.y);*/
            while (!rb.velocity.Equals(new Vector2(0, 0))) 
            {
                if (rb.velocity.x > 0 && rb.velocity.x < 0.09f) rb.velocity = new Vector2(0, rb.velocity.y);
                if (rb.velocity.y < 0.09f && rb.velocity.y > 0) rb.velocity = new Vector2(rb.velocity.x,0);
                if (rb.velocity.x > -0.09f && rb.velocity.x < 0) rb.velocity = new Vector2(0,rb.velocity.y);
                if (rb.velocity.y > -0.09f && rb.velocity.y < 0) rb.velocity = new Vector2(rb.velocity.x,0);

                /* velo.x--;
                 velo.x = Mathf.Clamp(velo.x, 0, Mathf.Infinity);
                 velo.y--;
                 velo.y = Mathf.Clamp(velo.y, 0, Mathf.Infinity);
                 Debug.Log(rb.velocity);
                 Debug.Log(velo);*/

                rb.velocity /= 1.0667f;

                yield return new WaitForEndOfFrame();
                
            }

            rb.isKinematic = true;
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
