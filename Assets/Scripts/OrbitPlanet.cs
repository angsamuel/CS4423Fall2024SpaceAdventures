using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitPlanet : MonoBehaviour
{

    [SerializeField] Transform planetProper;
    [SerializeField] Transform following;
    [SerializeField] float orbitSpeed = 10;

    [SerializeField] SpriteRenderer spriteRenderer;


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

    public void RandomizeLook(){
        //SpriteRenderer sr = planetProper.GetComponent<SpriteRenderer>();
        //sr.color = Color.HSVToRGB(Random.Range(0f,1f),.5f,.5f);
        spriteRenderer.material.SetColor("_BaseColor", new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
        spriteRenderer.material.SetColor("_AtmosphereColor", new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
        spriteRenderer.material.SetFloat("_TerrainScale1", Random.Range(1f, 6f));
        spriteRenderer.material.SetFloat("_TerrainScale2", Random.Range(1f, 6f));
        spriteRenderer.material.SetFloat("_CloudScale1", Random.Range(1f, 6f));
        spriteRenderer.material.SetFloat("_CloudScale2", Random.Range(1f, 6f));
        spriteRenderer.material.SetFloat("_CloudScale222222", Random.Range(1f, 6f));
    }

    public Transform GetPlanetProper(){
        return planetProper;
    }


}
