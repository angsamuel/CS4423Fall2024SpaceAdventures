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
    [SerializeField] float maxReloadTime = 10;
    [SerializeField] float cooldownTime = .25f;
    float currentReloadTime = 0;

    void Awake(){
        currentAmmo = maxAmmo;
    }

    bool coolingDown = false;
    //launch a projectile forward
    public float Launch(){ //returns a recoil amount

        if(currentAmmo < 1){
            return 0;
        }

        if(currentReloadTime > 0){
            return 0;
        }

        if(coolingDown){
            return 0;
        }
        Cooldown();

        currentAmmo -= 1;
        GameObject newProjectile = Instantiate(projectilePrefab, spawnTransform.position, Quaternion.identity);
        newProjectile.GetComponent<Rigidbody2D>().velocity = transform.up * projectileSpeed;
        audioSource.pitch = Random.Range(1f-pitchRange,1f+pitchRange);
        audioSource.Play();

        Destroy(newProjectile,2);
        return GetRecoilAmount();
    }

    void Cooldown(){
        coolingDown = true;
        StartCoroutine(CoolingDownRoutine());
        IEnumerator CoolingDownRoutine(){
            yield return new WaitForSeconds(cooldownTime);
            coolingDown = false;
        }
    }



    bool currentlyReloading = false;
    public void Reload(){

        if(currentlyReloading){
            return;
        }
        if(currentAmmo == maxAmmo){
            return;
        }
        currentlyReloading = true;
        currentReloadTime = 0;
        StartCoroutine(ReloadRoutine());

        IEnumerator ReloadRoutine(){
            Debug.Log("Reload Routine Active!");
            //yield return new WaitForSeconds(reloadTime);

            while(currentReloadTime < maxReloadTime){
                yield return null;
                currentReloadTime += Time.deltaTime;
            }
            currentReloadTime = 0;
            currentAmmo = maxAmmo;
            currentlyReloading = false;
            Debug.Log("Reload Routine Done!");
        }

    }

    public float GetReloadPercentage(){
        return currentReloadTime / maxReloadTime;
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
