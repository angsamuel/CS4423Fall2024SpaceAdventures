using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NoticeText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    // Start is called before the first frame update

    void Start(){
        HideText();
    }
    public void ShowText(){
        text.gameObject.SetActive(true);
    }

    public void HideText(){
        text.gameObject.SetActive(false);
    }
}
