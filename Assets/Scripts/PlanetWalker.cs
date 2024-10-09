using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetWalker : MonoBehaviour
{

    [SerializeField] int direction = 1;
    [SerializeField] float speed = 50;
    [SerializeField] AnimationStateChanger animationStateChanger;
    [SerializeField] SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        PatrolPlanet();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PatrolPlanet(){

        StartCoroutine(PatrolPlanetRoutine());
        IEnumerator PatrolPlanetRoutine(){
            float timer = 0;
            float selectedTime = Random.Range(5f, 10f);
            float pauseTime = Random.Range(1f, 3f);
            while (true){
                yield return null;
                timer+=Time.deltaTime;

                if(timer > pauseTime){
                    transform.Rotate(0, 0, speed * direction * Time.deltaTime);
                    animationStateChanger.ChangeAnimtionState("Walk");
                    spriteRenderer.flipX = (direction == 1);
                }
                else{
                    animationStateChanger.ChangeAnimtionState("Idle");
                }


                if(timer > selectedTime + pauseTime){
                    direction *= -1;
                    timer = 0;
                    pauseTime = Random.Range(1f,3f);
                    selectedTime = Random.Range(5f,10f);
                }


            }
            yield return null;
        }
    }

    //walk
    //pause
    //walk in opposite direction
}
