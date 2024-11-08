using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] SpriteRenderer healthBar;
    [SerializeField] SpriteRenderer bodySprite;
    [SerializeField] float maxHealth = 100;
    [SerializeField] float currentHealth = 100;
    bool dead = false;

    void Start(){
        //healthBar.material = new Material(healthBar.material);
        SetHealthBar();
    }

    public void SetHealthBar(){
        if(healthBar != null){
            float value = currentHealth / maxHealth;
            healthBar.material.SetFloat("_Value", value);
        }
    }

    public void TakeDamage(float amount){
        SetCurrentHealth(currentHealth - amount);
        if(currentHealth <= 0){
            currentHealth = 0;
            bodySprite.color = Color.black;
            dead = true;
        }
    }

    public void SetCurrentHealth(float amount){
        currentHealth = amount;
        SetHealthBar();
    }

    public void SetMaxHealth(float amount){
        maxHealth = amount;
        SetCurrentHealth(maxHealth);
    }

    public void Heal(float amount){
        SetCurrentHealth(currentHealth + amount);
        if(currentHealth > maxHealth){
            currentHealth = maxHealth;
        }
    }

    public bool IsDead(){
        return dead;
    }
}
