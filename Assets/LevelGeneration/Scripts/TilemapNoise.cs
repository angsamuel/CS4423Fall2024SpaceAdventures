using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; //remember to include this when working with tilemaps!

public class TilemapNoise : MonoBehaviour
{


    [Header("Config")]
    public float noiseScale = .1f;
    public int areaSize = 100;


    public Tilemap tilemap;
    public TileBase tile;


    void Start(){
        ApplyNoise();
    }

    void ApplyNoise(){
        for(int x = 0; x<areaSize; x++){
            for(int y = 0; y<areaSize; y++){
                
                float perlinValue = Mathf.PerlinNoise((float)x*noiseScale,(float)y*noiseScale); //keep coordinates between 0 and 1
                
                if(perlinValue > .75f){
                    tilemap.SetTile(new Vector3Int(x,y),tile);
                }
            }
        }
    }


}
