using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] float projectileSpeed = 10.0f;

    [Header("Prefabs")]
    [SerializeField] GameObject projectilePrefab;

    [Header("Helpers")]
    [SerializeField] Transform spawnTransform;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [Range(0,1)]
    [SerializeField] float pitchRange = .2f;

    [Header("Ammo")]
    [SerializeField] int maxAmmo = 10;
    [SerializeField] int currentAmmo = 10;

    void Awake(){
        currentAmmo = maxAmmo;
    }

    //launch a projectile forward
    public void Launch(){

        if(currentAmmo < 1){
            return;
        }

        currentAmmo -= 1;
        GameObject newProjectile = Instantiate(projectilePrefab, spawnTransform.position, Quaternion.identity);
        newProjectile.GetComponent<Rigidbody2D>().velocity = transform.up * projectileSpeed;
        audioSource.pitch = Random.Range(1f-pitchRange,1f+pitchRange);
        audioSource.Play();

        Destroy(newProjectile,2);
    }

    public float GetRecoilAmount(){
        return projectileSpeed * 2;
    }

    public int GetAmmo(){
        return currentAmmo;
    }
    public int GetMaxAmmo(){
        return maxAmmo;
    }
}
