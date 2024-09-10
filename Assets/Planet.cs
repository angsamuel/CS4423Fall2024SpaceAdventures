using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] Color defaultColor;
    [SerializeField] Color visitColor;
    SpriteRenderer spriteRenderer;

    [SerializeField] int visitors = 0;


    void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = defaultColor;
        string a = "hhh";
        StringTest(a);
        Debug.Log(a);
    }

    void StringTest(string wowza){
        wowza = "hello baby";
    }

    void OnTriggerEnter2D(Collider2D other){
        spriteRenderer.color = visitColor;
        visitors+=1;
    }

    void OnTriggerExit2D(Collider2D other){
        visitors-=1;
        if(visitors < 1){
            spriteRenderer.color = defaultColor;
            visitors = 0;
        }


    }

}
