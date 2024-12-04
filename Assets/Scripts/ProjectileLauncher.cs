using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{

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
    [SerializeField] float projectileSpeed = 10.0f;
    [SerializeField] float recoil = 2;
    [SerializeField] float projectileSize = 1;
    [SerializeField] float accuracy = 1; //1 for perfect accracy, 0 for no accuracy
    [SerializeField] int projectileCount = 1;
    [SerializeField] float projectileDamage = 1;
    float currentReloadTime = 0;

    [Header("Proc Gen")]
    [SerializeField] int seed = 0;

    int minGenAmmo = 1;
    int maxGenAmmo = 12;
    float minGenReloadTime = 1;
    float maxGenReloadTime = 3;
    float minGenCooldownTime = .01f;
    float maxGenCooldownTime = 1f;
    float minGenRecoil = 0;
    float maxGenRecoil = 2;
    float minGenSize = .1f;
    float maxGenSize = .5f;
    float minGenProjectileSpeed = 2.5f;
    float maxGenProjectileSpeed = 10.0f;
    float minGenDamage = 1;
    float maxGenDamage = 50;

    //trackers
    SpaceShip ownerShip;


    void Awake(){
        currentAmmo = maxAmmo;
        Generate(Random.Range(int.MinValue, int.MaxValue));
    }

    public void Generate(int newSeed){
        seed = newSeed;
        Random.InitState(newSeed);
        maxAmmo = Random.Range(minGenAmmo,maxGenAmmo+1);
        currentAmmo = maxAmmo;
        maxReloadTime = Random.Range(minGenReloadTime,maxGenReloadTime);
        cooldownTime = Random.Range(minGenCooldownTime,maxGenCooldownTime);
        recoil = Random.Range(minGenRecoil,maxGenRecoil);
        projectileSpeed = Random.Range(minGenProjectileSpeed,maxGenProjectileSpeed);
        projectileSize = Random.Range(minGenSize,maxGenSize);
        accuracy = Random.Range(.5f,1f);
        projectileCount = Random.Range(1,10);
        projectileDamage = Random.Range(minGenDamage,maxGenDamage);
    }

    public void Equip(SpaceShip s){
        s.SetProjectileLauncher(this);
        ownerShip = s;
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
        for(int i = 0; i<projectileCount; i++){
            GameObject newProjectile = Instantiate(projectilePrefab, spawnTransform.position, transform.rotation);
            newProjectile.GetComponent<Damage>().SetAmount(projectileDamage);
            newProjectile.GetComponent<Damage>().SetSourceHealth(ownerShip.GetHealth());
            newProjectile.transform.localScale = Vector3.one * projectileSize;
            newProjectile.transform.Rotate(new Vector3(0,0,Random.Range(-90+(90*accuracy),90-(90*accuracy))));

            newProjectile.GetComponent<Rigidbody2D>().velocity = newProjectile.transform.up * projectileSpeed;
            Destroy(newProjectile,20);
        }

        audioSource.pitch = Random.Range(1f-pitchRange,1f+pitchRange);
        audioSource.Play();


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
        if(!ownerShip.TakeCargo(10)){
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
        return recoil;
    }

    public int GetAmmo(){
        return currentAmmo;
    }
    public int GetMaxAmmo(){
        return maxAmmo;
    }
}
