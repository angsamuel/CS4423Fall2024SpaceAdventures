using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SolarSystemManager : MonoBehaviour
{

    public static SolarSystemManager singleton;

    [Header("UI")]
    [SerializeField] NoticeText jumpText;

    [Header("Planets")]
    [SerializeField] List<Planet> planets;
    [SerializeField] int planetsColonized = 0;
    [SerializeField] ScreenFader hyperJumpScreenFader;
    bool colonizedEntireSystem = false;

    void Awake(){
        if(singleton == null){
            singleton = this;
        }else{
            Debug.LogError("Multiple Solar System Managers in da scene >:|");
            Destroy(this.gameObject);
        }
    }

    public void RegisterPlanet(Planet p){
        planets.Add(p);
    }

    public void ReportPlanetColonization(){
        planetsColonized+=1;
        if(planetsColonized == planets.Count){
            //We win!
            colonizedEntireSystem = true;
            jumpText.ShowText();
        }
    }

    public void JumpAwayFromSystem(){
        if(!colonizedEntireSystem){
            return;
        }
        hyperJumpScreenFader.FadeToColor();
        StartCoroutine(DelayLeaveLevelAfterJump());
    }

    IEnumerator DelayLeaveLevelAfterJump(){

        yield return new WaitUntil(()=>hyperJumpScreenFader.DoneFadingToColor());
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Load the scene
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //we could also do something like this to check if we won the game
        // bool winGame = true;
        // for(int i = 0; i<planets.Count; i++){
        //     if(planet.notColonized()){
        //         winGame= false;
        //     }
        // }
        // if(winGame){
        //     //do something
        // }
    }
}
