using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitPlanet : MonoBehaviour
{

    [SerializeField] Transform planetProper;
    [SerializeField] Transform following;
    [SerializeField] float orbitSpeed = 10;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] string PlanetName = "Meepis";


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = following.position;
        transform.Rotate(new Vector3(0,0,orbitSpeed*Time.fixedDeltaTime));
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
    }

    public void SavePlanet(){
        NDSaveLoad.SetFileName(PlanetName + ".txt");
        NDSaveLoad.SaveColor(PlanetName + "_BaseColor", spriteRenderer.material.GetColor("_BaseColor"));
        NDSaveLoad.SaveColor(PlanetName + "_AtmosphereColor", spriteRenderer.material.GetColor("_AtmosphereColor"));
        NDSaveLoad.SaveFloat(PlanetName + "_TerrainScale1", spriteRenderer.material.GetFloat("_TerrainScale1"));
        NDSaveLoad.SaveFloat(PlanetName + "_TerrainScale2", spriteRenderer.material.GetFloat("_TerrainScale2"));
        NDSaveLoad.SaveFloat(PlanetName + "_CloudScale1", spriteRenderer.material.GetFloat("_CloudScale1"));
        NDSaveLoad.SaveFloat(PlanetName + "_CloudScale2", spriteRenderer.material.GetFloat("_CloudScale2"));
        NDSaveLoad.Flush();
    }

    public void LoadPlanet()
    {
        NDSaveLoad.SetFileName(PlanetName + ".txt");
        spriteRenderer.material.SetColor("_BaseColor",  NDSaveLoad.LoadColor(PlanetName + "_BaseColor"));
        spriteRenderer.material.SetColor("_AtmosphereColor", NDSaveLoad.LoadColor(PlanetName + "_AtmosphereColor"));
        spriteRenderer.material.SetFloat("_TerrainScale1", NDSaveLoad.LoadFloat(PlanetName + "_TerrainScale1"));
        spriteRenderer.material.SetFloat("_TerrainScale2", NDSaveLoad.LoadFloat(PlanetName + "_TerrainScale2"));
        spriteRenderer.material.SetFloat("_CloudScale1", NDSaveLoad.LoadFloat(PlanetName + "_CloudScale1"));
        spriteRenderer.material.SetFloat("_CloudScale2", NDSaveLoad.LoadFloat(PlanetName + "_CloudScale2"));
    }

    public Transform GetPlanetProper(){
        return planetProper;
    }


}
