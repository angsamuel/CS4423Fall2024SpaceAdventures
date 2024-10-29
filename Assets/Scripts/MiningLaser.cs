using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningLaser : MonoBehaviour
{
    SpaceShip owner;
    bool onCooldown = false;

    void Awake(){
        owner = GetComponent<SpaceShip>();
    }

    public void Mine(Asteroid asteroid){
        if(onCooldown){
            return;
        }
        onCooldown = true;
        Cooldown();
        asteroid.transform.localScale *= .99f;
        if(asteroid.transform.localScale.magnitude < 1){
            Destroy(asteroid.gameObject);
        }else{
            owner.AddToCargo(1);
        }
    }

    void Cooldown(){
        StartCoroutine(CooldownRoutine());
        IEnumerator CooldownRoutine(){
            yield return new WaitForSeconds(1f);
            onCooldown = false;
        }
    }
}
