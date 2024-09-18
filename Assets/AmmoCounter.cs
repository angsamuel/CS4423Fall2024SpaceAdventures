using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoCounter : MonoBehaviour
{

    [SerializeField] PlayerInputHandler playerInputHandler;
    [SerializeField] TextMeshProUGUI ammoText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int ammoAmount = playerInputHandler.GetPlayerShip().GetProjectileLauncher().GetAmmo();
        int maxAmmo = playerInputHandler.GetPlayerShip().GetProjectileLauncher().GetMaxAmmo();
        float fraction = ((float)ammoAmount / (float)maxAmmo);
        ammoText.text = ammoAmount.ToString();
        ammoText.color = Color.Lerp(Color.yellow,Color.white,fraction);
        if(ammoAmount == 0){
            ammoText.color = Color.red;
        }
        // if(ammoAmount > 0){
        //     ammoText.color = Color.white;
        // }else{
        //     ammoText.color = Color.red;
        // }
    }
}
