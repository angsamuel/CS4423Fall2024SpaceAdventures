using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{

    Rigidbody2D rb;

    [SerializeField] float speed = 5;
    [SerializeField] float speedLimit = 10;

    [Header("Tools")]
    [SerializeField] ProjectileLauncher projectileLauncher;



    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //transform.localEulerAngles = new Vector3(0,0,45);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AimShip(Transform targetTransform){
       //transform.rotation =  Quaternion.LookRotation(Vector3.forward, targetTransform.position - transform.position);
        AimShip(targetTransform.position);
    }
    public void AimShip(Vector3 aimPos){
        transform.rotation = Quaternion.LookRotation(Vector3.forward, aimPos - transform.position);
    }

    void FixedUpdate(){
        if (rb.velocity.magnitude > speedLimit){
            rb.velocity = rb.velocity.normalized * speedLimit;
        }
    }

    public void Move(Vector3 movement)
    {
        rb.AddForce(movement * speed);
    }

    public void Recoil(Vector3 amount){
        rb.AddForce(amount, ForceMode2D.Impulse);
    }

    public void LaunchWithShip(){
        if(projectileLauncher.GetAmmo() > 0){
            Recoil(-transform.up * projectileLauncher.GetRecoilAmount());
        }
        projectileLauncher.Launch();
    }

    public ProjectileLauncher GetProjectileLauncher(){
        return projectileLauncher;
    }
}
