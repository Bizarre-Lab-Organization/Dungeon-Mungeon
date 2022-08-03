using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonMungeon
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int maxHP;
        private HealthSystem hp;
        private bool isPoisoned;

        void Awake()
        {
            isPoisoned = false;
            hp = new HealthSystem(maxHP);
        }

        void Update()
        {
            Debug.Log(isPoisoned);
            if (hp.GetHealth() == 0) Die();
        }

        public bool PlayerPoisoned
        {
            get { return isPoisoned; }
            set { isPoisoned = value; }
        }
        private void OnHit(int damage)
        {
            hp.TakeDamage(damage);
        }

        private void OnHeal(int heal)
        {
            if(hp.GetHealth() < maxHP)
            {
                hp.Heal(heal);
            }
        }

        private void OnPoisonHit(int dmg, int time)
        {
            if(isPoisoned == false)
            {
                isPoisoned = true;
                StartCoroutine(hp.Poison(dmg, time));
            }
        }

        private void Die()
        {
            if(gameObject != null)
            {
                Destroy(gameObject);
                Debug.Log("u ded");
            }
        }
        
    }
}
