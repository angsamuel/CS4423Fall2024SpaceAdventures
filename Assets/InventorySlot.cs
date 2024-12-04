using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InventorySlot : MonoBehaviour
{

    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] Color selectColor;
    [SerializeField] Color deselectColor;
    // Start is called before the first frame update
    public void Deselect(){
        image.color = deselectColor;
    }
    public void Select(){
        image.color = selectColor;
    }

    public void FillText(string newText){
        titleText.text = newText;
    }
}
