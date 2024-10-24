using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] float amount = 1;
    [SerializeField] Health sourceHealth;

    public void SetAmount(float newAmount){
        amount = newAmount;
    }
    public void SetSourceHealth(Health newHealth){
        sourceHealth = newHealth;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Ship")){
            if(other.GetComponent<Health>() == sourceHealth){ //no self damage
                return;
            }
            other.GetComponent<Health>().TakeDamage(amount);
            Destroy(this.gameObject);
        }
    }

}
