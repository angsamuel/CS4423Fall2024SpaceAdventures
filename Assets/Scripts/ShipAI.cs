using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAI : MonoBehaviour
{
    [SerializeField] string currentStateString;
    [SerializeField] SpaceShip myShip;
    [SerializeField] SpaceShip targetShip;

    [Header("Config")]
    [SerializeField] float sightDistance = 10;

    delegate void AIState();
    AIState currentState;


    //trackers==================================================
    float stateTime = 0;
    bool justChangedState = false;
    Vector3 lastTargetPos;


    // Start is called before the first frame update
    void Start()
    {
        ChangeState(IdleState);
    }


    void ChangeState(AIState newAIState){
        currentState = newAIState;
        justChangedState = true;
    }

    bool CanSeeTarget(){
        if(targetShip == null){
            return false;
        }

        return Vector3.Distance(myShip.transform.position, targetShip.transform.position) < sightDistance;
    }

    void FindTarget(){
        
        List<SpaceShip> potentialTargets = SolarSystemManager.singleton.GetSpaceShips();
        foreach(SpaceShip s in potentialTargets){
            if(s.GetTeam() == myShip.GetTeam()){
                continue;
            }
            
            if(s.IsDead()){
                continue;
            }

            if(targetShip == null){
                targetShip = s;
            }

            if(Vector3.Distance(targetShip.transform.position, myShip.transform.position) > Vector3.Distance(s.transform.position, myShip.transform.position)){
                targetShip = s;
            }

        }
    }

    void IdleState(){
        if (stateTime == 0)
        {
            currentStateString = "IdleState";
        }
        FindTarget();
        if (CanSeeTarget())
        {
            ChangeState(AttackState);
            return;
        }
       
    }

    void AttackState(){
        if(Vector3.Distance(transform.position,targetShip.transform.position) > 3){
            myShip.MoveToward(targetShip.transform.position);
        }else{
            myShip.Stop();
        }
        
        myShip.AimShip(targetShip.transform);
        if (stateTime == 0)
        {
            currentStateString = "AttackState";
        }

        if (stateTime > 1.5f){
            myShip.LaunchWithShip(); //shoot at player!
        }

        if(myShip.GetProjectileLauncher().GetAmmo() == 0){
            myShip.GetProjectileLauncher().Reload();
        }

        if(targetShip.IsDead()){
            targetShip = null;
            Debug.Log("TARGET DEAD");
            ChangeState(PatrolState);
            return;
        }else{
            Debug.Log(targetShip.gameObject.name);
        }

        if(!CanSeeTarget())
        {
            lastTargetPos = targetShip.transform.position;
            ChangeState(GetBackToTargetState);
            return;
        }
    }


    void GetBackToTargetState(){ //if we lose sight of the player, go back to the position where we last saw the player
        if (stateTime == 0)
        {
            currentStateString = "BackToTargetState";
        }

        myShip.MoveToward(lastTargetPos);
        myShip.AimShip(lastTargetPos);

        if(stateTime < 2){
            return;
        }


        if (CanSeeTarget())
        {
            ChangeState(AttackState);
            return;
        }
        if(Vector3.Distance(myShip.transform.position, lastTargetPos) < 1){
            ChangeState(PatrolState);
            return;
        }
    }


    Vector3 patrolPos;
    Vector3 patrolPivot; //where we started Patrolling
    void PatrolState(){
        if(stateTime == 0){
            targetShip = null;
            currentStateString = "PatrolState";
            patrolPivot = myShip.transform.position;
            patrolPos = myShip.transform.position + new Vector3(Random.Range(-sightDistance, sightDistance),Random.Range(-sightDistance, sightDistance));
        }

        myShip.MoveToward(patrolPos);
        myShip.AimShip(patrolPos);
        FindTarget();

        if (CanSeeTarget())
        {
            ChangeState(AttackState);
            return;
        }

        if (Vector3.Distance(myShip.transform.position,patrolPos) < 1f){
            patrolPos = patrolPivot + new Vector3(Random.Range(-sightDistance, sightDistance), Random.Range(-sightDistance, sightDistance));
            return;
        }

    }

    void AITick(){
        if(justChangedState){
            stateTime = 0;
            justChangedState = false;
        }
        currentState();
        stateTime += Time.deltaTime;

    }

    void FixedUpdate(){
        //Move the ship inside here/
        //set the mvoement instead of calling move ship
    }

    // Update is called once per frame
    void Update()
    {
        AITick();
    }


}
