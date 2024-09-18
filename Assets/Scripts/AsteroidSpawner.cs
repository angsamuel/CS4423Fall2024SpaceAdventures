using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

    [SerializeField] List<GameObject> asteroidPrefabs;

    [SerializeField] Transform playerTransform;

    [SerializeField] float spawnTime = 1f;
    [SerializeField] float startAsteroids = 10;
    [SerializeField] int maxAsteroids = 100;
    [SerializeField] float spawnDistance = 100;




    public float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        SpawnStartingAsteroids();
        SpawnAsteroids();
    }

    void SpawnAsteroid(){
        Vector3 spawnPos = playerTransform.position + new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),0).normalized * spawnDistance;
        Instantiate(asteroidPrefabs[Random.Range(0, asteroidPrefabs.Count)], spawnPos, Quaternion.identity);
    }

    void SpawnAsteroidNearPlayer(){
        Vector3 spawnPos = playerTransform.position + new Vector3(Random.Range(-spawnDistance, spawnDistance), Random.Range(-spawnDistance, spawnDistance), 0);
        Instantiate(asteroidPrefabs[Random.Range(0, asteroidPrefabs.Count)], spawnPos, Quaternion.identity);
    }

    // Update is called once per frame
    // void Update(){
    //     timer += Time.deltaTime;
    //     if(timer >= spawnTime){
    //         SpawnAsteroid();
    //         timer = 0;
    //     }
    // }

    void SpawnStartingAsteroids(){
        for(int i = 0; i<startAsteroids; i++){
            SpawnAsteroidNearPlayer();
        }
    }


    void SpawnAsteroids(){
        StartCoroutine(SpawnAsteroidsRoutine());
        IEnumerator SpawnAsteroidsRoutine()
        {
            int counter = 0;
            while (counter < maxAsteroids)
            {
                yield return new WaitForSeconds(spawnTime);
                SpawnAsteroid();
                counter ++;
            }

            // SpawnAsteroid();
            // yield return new WaitForSeconds(4);
            // yield return null; //waits for a single frame
            // yield return new WaitForFixedUpdate(); //wait for a physics update 1/50th second by default
            // SpawnAsteroid();
        }
    }



}
