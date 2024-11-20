using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScreenSettings : MonoBehaviour
{
    [SerializeField] TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    void Start(){

        resolutions = Screen.resolutions;
        Resolution currentResolution = Screen.currentResolution;
        int currentResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", resolutions.Length - 1);
        for (int i = 0; i<resolutions.Length; i++){
            string resolutionString = resolutions[i].width.ToString() + "x" + resolutions[i].height.ToString();
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(resolutionString));
        }
        currentResolutionIndex = Math.Min(currentResolutionIndex,resolutions.Length-1);
        resolutionDropdown.value = currentResolutionIndex;
        SetResolution();
    }

    public void SetResolution(){
        int rezIndex = resolutionDropdown.value;
        Screen.SetResolution(resolutions[rezIndex].width,resolutions[rezIndex].height,true);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionDropdown.value);
    }
}
