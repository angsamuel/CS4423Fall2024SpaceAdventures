using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalkRoom : MonoBehaviour
{
    public GameObject upDoor;
    public GameObject downDoor;
    public GameObject rightDoor;
    public GameObject leftDoor;

    public SpriteRenderer floor;
    public List<SpriteRenderer> walls;

    public void OpenUpDoor(){
        upDoor.SetActive(false);
    }
    public void OpenDownDoor(){
        downDoor.SetActive(false);
    }
    public void OpenLeftDoor(){
        leftDoor.SetActive(false);
    }
    public void OpenRightDoor(){
        rightDoor.SetActive(false);
    }

}
