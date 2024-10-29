using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitPlanet : MonoBehaviour
{

    [SerializeField] Transform planetProper;
    [SerializeField] Transform following;
    [SerializeField] float orbitSpeed = 10;
    

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = following.position;
        transform.Rotate(new Vector3(0,0,orbitSpeed*Time.deltaTime));
    }

    public void SetFollow(Transform newFollow){
        following = newFollow;
    }

    public void SetOrbitDistance(float distance){
        planetProper.transform.localPosition = new Vector3(0,distance,0);
    }

    public void SetOrbitSpeed(float newOrbitSpeed){
        orbitSpeed = newOrbitSpeed;
    }

    public void SetScale(float newScale){
        planetProper.transform.localScale = Vector3.one * newScale;
    }

    public void SetRandomRotation(){
        transform.localEulerAngles = new Vector3(0,0,Random.Range(0f,360f));
    }

    public void RandomizeColor(){
        SpriteRenderer sr = planetProper.GetComponent<SpriteRenderer>();
        sr.color = Color.HSVToRGB(Random.Range(0f,1f),.5f,.5f);
    }

    public Transform GetPlanetProper(){
        return planetProper;
    }


}
