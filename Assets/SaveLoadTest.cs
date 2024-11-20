using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveLoadTest : MonoBehaviour
{
    [SerializeField] OrbitPlanet orbitPlanet;

    // Start is called before the first frame update
    void Start()
    {
        NDSaveLoad.SetFileName("space_test.txt");
        NDSaveLoad.SaveInt("player level",6);
        NDSaveLoad.Flush();
        NDSaveLoad.LoadFromFile();
        int playerLevel = NDSaveLoad.LoadInt("player level");
        Debug.Log($"player level is {playerLevel}");

        //PlayerPrefs.SetFloat("volume level", 0.5f);
        //float volumeLevel = PlayerPrefs.GetFloat("volume level");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            orbitPlanet.RandomizeLook();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            orbitPlanet.SavePlanet();
        }
        if(Input.GetKeyDown(KeyCode.L)){
            orbitPlanet.LoadPlanet();
        }
    }
}
