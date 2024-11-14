using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMaterialGenerator : MonoBehaviour
{

    Material material;
    void Awake(){
        material = GetComponent<SpriteRenderer>().material;
    }

    public void Generate(){

    }
}
