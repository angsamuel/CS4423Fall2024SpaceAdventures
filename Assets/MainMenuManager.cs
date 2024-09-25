using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame(){
        SceneManager.LoadScene("Space");
    }

    public void QuitGame(){
        Debug.Log("Quit");
        Application.Quit();
    }
}
