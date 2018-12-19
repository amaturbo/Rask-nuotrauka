using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

    public float startHealth;
    public float currentHealth;
    public Image healthBar;

    public EnemyHealth(float health)
    {
        startHealth = health;
    }
	
	void Start () {
        currentHealth = startHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void takeDamage(float amount)
    {
        if (amount < 0) return;
        currentHealth -= amount;
        healthBar.fillAmount = currentHealth / startHealth;

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
