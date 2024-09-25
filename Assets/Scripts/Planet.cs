using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] Color defaultColor;
    [SerializeField] Color visitColor;

    [SerializeField] Color colonizedColor;
    SpriteRenderer spriteRenderer;

    [SerializeField] int visitors = 0;

    [Header("Colonization")]
    [SerializeField] Transform colonizeProgressTransform;
    [SerializeField] SpriteRenderer colonizeProgressSpriteRenderer;
    [SerializeField] float colonizeTime = 5;
    float colonizeProgressPercentage = 0;



    void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = defaultColor;

    }

    public void SetColonizeProgress(float t){ //t should be from 0 to 1
        colonizeProgressTransform.localScale = Vector3.one * t;
    }

    // void OnTriggerStay2D(Collider2D other) {
    //     //once per fixed update
    //     colonizeProgressPercentage += Time.fixedDeltaTime * colonizeSpeed;
    //     if(colonizeProgressPercentage > 1){
    //         colonizeProgressPercentage = 1;
    //     }
    //     SetColonizeProgress(colonizeProgressPercentage);
    // }

    void Update(){
        colonizeProgressPercentage += (Time.deltaTime / colonizeTime) * visitors;
        if(colonizeProgressPercentage > 1){
            colonizeProgressSpriteRenderer.color = colonizedColor;
            colonizeProgressPercentage = 1;
        }
        SetColonizeProgress(colonizeProgressPercentage);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Ship")){
            spriteRenderer.color = visitColor;
            visitors+=1;
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Ship"))
        {
            visitors -=1;
            if(visitors < 1){
                spriteRenderer.color = defaultColor;
                visitors = 0;
            }
        }
    }

}
