using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{

    public float startingHealth;
    protected float health;
    protected bool dead;
    // Start is called before the first frame update

    protected virtual void Start()
    {
        health = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TakeDamge(float damage)
    {
        health -= damage;
        if (health <= 0 && !dead)
        {
            Die();
        }
    }


    protected void Die()
    {
        dead = true;
        GameObject.Destroy(gameObject);
    }
}
