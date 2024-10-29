using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public GameObject door;

    public void OpenDoor(){
        door.SetActive(false);
    }

    public void CloseDoor(){
        door.SetActive(true);
    }
}
