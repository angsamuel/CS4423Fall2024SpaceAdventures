using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapPlatformerNoise : MonoBehaviour
{

    public float noiseScale = .1f;
    public int ySeed = 0;
    public int startX;
    public int endX;
    public int maxY = 10; //how high each terrain column can be

    public Tilemap tilemap;
    public TileBase tile;

    void Start(){
        ApplyPlatformerNoise();
    }

    void ApplyPlatformerNoise(){
        for(int x = startX; x<endX; x++){//for each column
            float perlinValue = Mathf.PerlinNoise((float)x*noiseScale,(float)ySeed*noiseScale); //keep coordinates between 0 and 1, y stays the same
            
            //make column as high as noise dictates
            int localMaxY = (int)((float)maxY * perlinValue);
            for(int y = 0; y<localMaxY; y++){
                tilemap.SetTile(new Vector3Int(x,y),tile);
            }
        }
    }
}
