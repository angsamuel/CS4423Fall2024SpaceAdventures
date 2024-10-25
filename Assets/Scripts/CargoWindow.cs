using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CargoWindow : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI cargoText;
    [SerializeField] PlayerInputHandler playerInputHandler;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cargoText.text = "CARGO     " + playerInputHandler.GetPlayerShip().GetCargo().ToString();
    }
}
