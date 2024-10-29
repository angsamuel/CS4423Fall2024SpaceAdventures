using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [SerializeField] float size = 10;
    [SerializeField] GameObject asteroidPrefab;

    bool canBreakApart = false;
    // Start is called before the first frame update
    SpriteRenderer spriteRenderer;
    void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        //Resize();
        //RandomizeVelocity();
        //RandomizeColor();
        SolarSystemManager.singleton.RegisterAsteroid(this);
    }

    void FixedUpdate(){
        canBreakApart = true;
    }

    public void Resize(float newSize){
        size = newSize;
        Resize();
    }

    public void Resize(){
        if(size < 1){
            Destroy(this.gameObject);
            return;
        }
        transform.localScale = Vector3.one * size;
    }

    public void RandomizeVelocity(){
        float maxSpeed = 5;
        GetComponent<Rigidbody2D>().velocity = new Vector3(Random.Range(-maxSpeed, maxSpeed),Random.Range(-maxSpeed, maxSpeed));
    }

    public void RandomizeColor(){


        float minColorVal = .25f;
        float maxColorVal = .5f;
        spriteRenderer.color = new Color(Random.Range(minColorVal,maxColorVal), Random.Range(minColorVal,maxColorVal), Random.Range(minColorVal,maxColorVal));
    }

    void BreakApart(){
        if(!canBreakApart){
            return;
        }
        float spawnRange = size/2;
        for(int i = 0; i<2; i++){
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange),0);
            GameObject newAsteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
            newAsteroid.GetComponent<Asteroid>().Resize(size/2);
        }

        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other){
        // if(other.CompareTag("Projectile")){
        //     Debug.Log("Break apart!");
        //     Destroy(other.gameObject);
        //     BreakApart();
        // }
        // }else if(other.CompareTag("Ship")){
        //     other.GetComponent<SpaceShip>().Damage();
        // }

    }

    void OnDestroy()
    {
        SolarSystemManager.singleton.RemoveAsteroid(this);
    }
}
