using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    [SerializeField] float currentHealth = 100;
    bool dead = false;

    public void TakeDamage(float amount){
        currentHealth -= amount;
        if(currentHealth <= 0){
            currentHealth = 0;
            dead = true;
        }
    }

    public void SetHealth(float amount){
        currentHealth = amount;
        maxHealth = amount;
    }

    public void Heal(float amount){
        currentHealth += amount;
        if(currentHealth > maxHealth){
            currentHealth = maxHealth;
        }
    }

    public bool IsDead(){
        return dead;
    }
}
