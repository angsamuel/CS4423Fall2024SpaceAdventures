using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NDSaveLoad.SetFileName("space_test.txt");
        NDSaveLoad.SaveInt("player level",5);
        int playerLevel = NDSaveLoad.LoadInt("player level");
        NDSaveLoad.Flush();

        PlayerPrefs.SetFloat("volume level", 0.5f);
        float volumeLevel = PlayerPrefs.GetFloat("volume level");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
