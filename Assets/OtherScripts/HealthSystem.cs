using UnityEngine;
using System.Collections;
public class HealthSystem
{
    private int health;
    private int healthMax;

    public HealthSystem(int healthMax)
    {
        this.healthMax = healthMax;
        health = healthMax;
    }

    public int GetHealth()
    {
        return health;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health < 0) health = 0;
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health > healthMax) health = healthMax;
    }
    public IEnumerator Poison(int damage, int time)
    {
        while (time > 0)
        {
            TakeDamage(damage);
            Debug.Log(health);
            yield return new WaitForSeconds(1);
            time--;
        }
    }

}

