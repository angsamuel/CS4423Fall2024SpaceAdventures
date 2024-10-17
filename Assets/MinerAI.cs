using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerAI : MonoBehaviour
{
    DecisionTree decisionTree;
    MiningLaser miningLaser;
    SpaceShip myShip;
    SpaceShip motherShip;

    Asteroid targetAsteroid;

    public void Awake(){
        decisionTree = GetComponent<DecisionTree>();
        miningLaser = GetComponent<MiningLaser>();
        myShip = GetComponent<SpaceShip>();
    }

    public void Start(){


        BTNode root = new BTNode();

        BTNode leaveActions = new BTNode();

        BTNode goToAsteroid = new BTNode();
        goToAsteroid.btState = GoToAsteroid;
        leaveActions.AddChild(goToAsteroid,CanGoToAsteroid);

        BTNode mineAsteroid = new BTNode();
        mineAsteroid.btState = MineAsteroid;
        leaveActions.AddChild(mineAsteroid, CanMineAsteroid);

        root.AddChild(leaveActions, CanLeave);

        BTNode dropoffDelivery = new BTNode();
        dropoffDelivery.btState = ReturnToShip;
        root.AddChild(dropoffDelivery, CanReturn);

        decisionTree.SetRoot(root);

    }

    void Update(){
        decisionTree.RunTree();
    }

    public void SetMotherShip(SpaceShip ms){
        motherShip = ms;
    }

    public bool CanGoToAsteroid(){
        if(myShip.CargoFull()){
            return false;
        }

        if(targetAsteroid != null && Vector3.Distance(targetAsteroid.transform.position,myShip.transform.position) < 1){
            return false;
        }

        List<Asteroid> asteroids = SolarSystemManager.singleton.GetASteroids();
        if(asteroids.Count > 0){
            targetAsteroid = asteroids[0];
            return true;
        }

        Debug.Log("NO ASTEROIDS");

        return false;
    }

    public bool CanLeave(){
        return CanGoToAsteroid() || CanMineAsteroid();
    }
    public bool CanReturn(){
        return myShip.CargoFull();
    }

    public BTNode.BTOutcome GoToAsteroid(){
        myShip.MoveToward(targetAsteroid.transform.position);
        myShip.AimShip(targetAsteroid.transform.position);

        if(targetAsteroid == null || Vector3.Distance(myShip.transform.position,targetAsteroid.transform.position) < 1){
            return BTNode.BTOutcome.SWITCH; //all done
        }

        return BTNode.BTOutcome.CONTINUE;
    }


    public bool CanMineAsteroid(){
        if (targetAsteroid == null)
        {
            return false;
        }

        if (myShip.CargoFull())
        {
            return false;
        }

        if(Vector3.Distance(myShip.transform.position, targetAsteroid.transform.position) > 10){
            return false;
        }

        return true;
    }

    public BTNode.BTOutcome MineAsteroid(){
        if(!CanMineAsteroid()){
            targetAsteroid = null;
            Debug.Log("SWITCHING BACK");
            return BTNode.BTOutcome.SWITCH;
        }

        miningLaser.Mine(targetAsteroid);
        return BTNode.BTOutcome.CONTINUE;
    }

    public BTNode.BTOutcome ReturnToShip(){
        myShip.MoveToward(motherShip.transform.position);
        myShip.AimShip(motherShip.transform.position);

        if(Vector3.Distance(myShip.transform.position,motherShip.transform.position) < 1){
            motherShip.TransferCargo(myShip);
            Destroy(this.gameObject);
            return BTNode.BTOutcome.SWITCH;
        }

        return BTNode.BTOutcome.CONTINUE;
    }






}
