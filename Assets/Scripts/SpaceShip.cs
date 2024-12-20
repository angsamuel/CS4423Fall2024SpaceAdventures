using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{

    Rigidbody2D rb;
    Health health;

    public enum Team{player, pirate}

    [Header("Visuals")]
    [SerializeField] Transform body;


    [Header("Social")]
    [SerializeField] Team team;
    [Header("Movement")]
    [SerializeField] float speed = 5;
    [SerializeField] float speedLimit = 10;

    [Header("Rotation")]
    [SerializeField] float rotationSpeed = 10;



    //Cargo
    [Header("Cargo")]
    [SerializeField] int currentCargo = 0;
    [SerializeField] int maxCargo = 100;
    [SerializeField] List<ProjectileLauncher> launchers;
    int currentLauncherIndex = 0;

    [Header("Mining")]
    [SerializeField] GameObject minerPrefab;
    //trackers

    Vector3 movement = Vector3.zero;




    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        launchers[currentLauncherIndex].Equip(this);
    }

    void Start(){
        SolarSystemManager.singleton.RegisterSpaceShip(this);
    }



    public void AimShip(Transform targetTransform){
       //body.transform.rotation =  Quaternion.LookRotation(Vector3.forward, targetTransform.position - transform.position);
        AimShip(targetTransform.position);
    }
    public void AimShip(Vector3 aimPos){
        if(health.IsDead()){
            return;
        }
        Quaternion goalRotation = Quaternion.LookRotation(Vector3.forward, aimPos - transform.position);
        Quaternion currentRotation = body.transform.rotation;

        body.transform.rotation = Quaternion.Lerp(currentRotation,goalRotation,Time.deltaTime * rotationSpeed);
        //body.transform.rotation = Quaternion.Slerp(currentRotation, goalRotation, Time.deltaTime * rotationSpeed);
    }

    void FixedUpdate(){
        if(health.IsDead()){
            return;
        }

        rb.AddForce(movement * speed);
        if (rb.velocity.magnitude > speedLimit){
            rb.velocity = rb.velocity.normalized * speedLimit;
        }
    }

    public void Move(Vector3 newMovement)
    {
       movement = newMovement;
    }
    public void Stop(){
        movement = Vector3.zero;
    }

    // void Die(){
    //     GetComponent<SpriteRenderer>().color = Color.black;
    // }




    public void MoveToward(Vector3 goalPos){
        goalPos.z = 0;
        Vector3 direction = goalPos - transform.position;
        Move(direction.normalized);
    }

    public void Recoil(Vector3 amount){
        rb.AddForce(amount, ForceMode2D.Impulse);
    }

    public void LaunchWithShip(){
        if(health.IsDead()){
            return;
        }
        Recoil(-transform.up * launchers[currentLauncherIndex].Launch());
    }

    public ProjectileLauncher GetProjectileLauncher(){
        return launchers[currentLauncherIndex];
    }



    public bool IsDead(){
        return health.IsDead();
    }

    public void AddToCargo(int amount){
        currentCargo += amount;

        if (currentCargo > maxCargo){
            currentCargo = maxCargo;
        }
    }

    public bool CargoFull(){
        return currentCargo >= maxCargo;
    }

    public int EmptyCargo(){
        int tempCargo = currentCargo;
        currentCargo = 0;
        return tempCargo;
    }

    public void TransferCargo(SpaceShip deliveryBoi){
        AddToCargo(deliveryBoi.EmptyCargo());
    }

    public void DeployMiner(){
        if(currentCargo < 1){
            return;
        }
        currentCargo -= 1;
        GameObject newMiner = Instantiate(minerPrefab,transform.position,Quaternion.identity);
        newMiner.GetComponent<MinerAI>().SetMotherShip(this);
    }

    public Health GetHealth(){
        return health;
    }

    public void SetProjectileLauncher(ProjectileLauncher pl){
        launchers[currentLauncherIndex] = pl;
    }

    public void SetTeam(Team newTeam){
        team = newTeam;
    }

    public Team GetTeam(){
        return team;
    }

    public int GetCargo(){
        return currentCargo;
    }

    public bool TakeCargo(int amount){
        if(currentCargo <amount){
            return false;
        }
        currentCargo -= amount;
        return true;
    }

    public void EquipNextInInventory(){

        currentLauncherIndex += 1;
        if(currentLauncherIndex > launchers.Count-1){
            currentLauncherIndex = 0;
        }
        launchers[currentLauncherIndex].Equip(this);
    }
    public List<ProjectileLauncher> GetAllLaunchers(){
        return launchers;
    }

}
