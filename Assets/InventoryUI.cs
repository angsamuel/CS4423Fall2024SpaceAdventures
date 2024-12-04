using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.WSA;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] SpaceShip playerShip;
    [SerializeField] List<InventorySlot> slots;
    [SerializeField] GameObject slotPrefab;

    void Start(){
        slots = new List<InventorySlot>();
        List<ProjectileLauncher> launchers = playerShip.GetAllLaunchers();
        Debug.Log(launchers.Count);
        for(int i = 0; i<launchers.Count; i++){
            slots.Add(Instantiate(slotPrefab,this.transform).GetComponent<InventorySlot>());
        }
    }

    void Update(){
        List<ProjectileLauncher> launchers = playerShip.GetAllLaunchers();
        for(int i = 0; i<launchers.Count; i++){
            slots[i].FillText(launchers[i].GetAmmo().ToString());
            slots[i].Deselect();
            if(launchers[i] == playerShip.GetProjectileLauncher()){
                slots[i].Select();
            }
        }
    }
}
