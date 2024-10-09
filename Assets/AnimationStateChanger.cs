using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateChanger : MonoBehaviour
{
    // public enum AnimationState{Idle, Walk};
    // [SerializeField] AnimationState currentEnumState = AnimationState.Idle


    [SerializeField] Animator animator;
    [SerializeField] string currentState = "Idle";

    void Start(){
        ChangeAnimtionState("Idle");
    }

    public void ChangeAnimtionState(string newState, float speed = 1){
        animator.speed = speed;
        if (currentState == newState){
            return;
        }
        currentState = newState;
        animator.Play(currentState);

    }

}
