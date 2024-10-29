using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModularLevelGenerator : MonoBehaviour
{

     
    public bool regen = false;

    [Header("Config")]
    public int seed = 0;
    public int roomsMin = 10;
    public int roomsMax = 30;
    public List<GameObject> genRooms;

    [Header("Trackers")]
    public int failures = 0;
    public int maxFailures = 100;
    bool generating = false;

    List<GameObject> placedRooms;
    Dictionary<Vector2Int,bool> placementDict;
    List<Exit> openExits;
    

    void Start(){
        
        StartCoroutine(RegenRoutine());
    }

    IEnumerator RegenRoutine(){
        while(true){
            generating = true;
            regen = false;
            StartCoroutine(Generate(seed));
            yield return new WaitUntil(()=>!generating);
            yield return new WaitUntil(()=>regen);
            Reset();
            seed+=1;

        }
        yield return null;
        
    }

    void Reset(){
        
        foreach(GameObject g in placedRooms){
            Destroy(g);
        }
    }

    IEnumerator Generate(int newSeed){
        Random.InitState(newSeed);

        //initialize
        placementDict = new Dictionary<Vector2Int, bool>();
        openExits = new List<Exit>();
        failures = 0;
        placedRooms = new List<GameObject>();

        //place all the rooms we'll be using
        List<GameObject> genRoomPalette = new List<GameObject>();
        for(int i = 0; i<genRooms.Count; i++){
            genRoomPalette.Add(Instantiate(genRooms[i],new Vector3(100,i*10,0),Quaternion.identity));
        }

        //place starter room
        GameObject starter = genRoomPalette[Random.Range(0,genRoomPalette.Count)];

        GenRoom starterRoom = Instantiate(starter, Vector3.zero,Quaternion.identity).GetComponent<GenRoom>();
        LockInRoom(starterRoom);
        starterRoom.gameObject.name = "Starter";



        int roomsToSpawn = Random.Range(roomsMin,roomsMax);

        for(int i = 0; i<roomsToSpawn; i++){
            
            //first select an exit
            int selectedExit = Random.Range(0,openExits.Count);
            
            //then select a new room type
            GenRoom nextGenRoom = genRoomPalette[Random.Range(0,genRoomPalette.Count)].GetComponent<GenRoom>();
            

            //position our next room where it should go
            nextGenRoom.transform.position = openExits[selectedExit].transform.position;

            //rotate to add f l a v o r
            nextGenRoom.transform.rotation = Quaternion.Euler(0,0,90*Random.Range(0,4));

            //check if it overlaps with an existing room
            if(CanPlaceRoom(nextGenRoom)){
                //if it does fit, plop it down, lock it in
                GenRoom copyRoom = Instantiate(nextGenRoom.gameObject,nextGenRoom.transform.position,nextGenRoom.transform.rotation).GetComponent<GenRoom>();
                copyRoom.gameObject.name = "Copy";
                
                //open the correct doors
                openExits[selectedExit].OpenDoor();
                foreach(Exit e in copyRoom.exits){
                    if(e.door.transform.position == openExits[selectedExit].door.transform.position){
                        e.OpenDoor();
                        copyRoom.exits.Remove(e);
                        break;
                    }
                }
                openExits.RemoveAt(selectedExit);
                nextGenRoom.transform.position += new Vector3(100,100,0);
                
                LockInRoom(copyRoom); //lock in the new room to prevent overlaps
                
                yield return new WaitForSeconds(.1f); //wait some time, timeslicing

            }else{
                //if not, increase failures
                failures += 1;
                i-=1;
                if(failures > maxFailures){
                    break;
                }
            }

            //move it out of the way so it looks pretty while it's generating
            nextGenRoom.transform.position += new Vector3(100,100,0);
            yield return null;
            
        
        }

        foreach(GameObject g in genRoomPalette){
            Destroy(g);
        }
        generating = false;

    }
    
    bool CanPlaceRoom(GenRoom genRoom){

        
        foreach(Transform t in genRoom.floors){   
            if(placementDict.ContainsKey(Vector2Int.RoundToInt(t.position))){
                return false;
            }
        }

        return true;
    }
    void LockInRoom(GenRoom genRoom){
        foreach(Exit e in genRoom.exits){
            openExits.Add(e);
        }
        foreach(Transform t in genRoom.floors){
            placementDict[Vector2Int.RoundToInt(t.position)] = true;
        }
        placedRooms.Add(genRoom.gameObject);
    }


}
