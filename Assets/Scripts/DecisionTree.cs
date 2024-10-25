using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionTree : MonoBehaviour
{

    BTNode root;
    BTNode currentNode;

    public void SetRoot(BTNode newRoot){
        root = newRoot;
    }
    public void FindLeaf(){
        BTNode chosenNode = root.SelectChild();
        if(chosenNode.IsLeaf()){
            currentNode = chosenNode;
        }else{
            currentNode = null;
        }
    }

    public void RunTree(){
        if(currentNode == null){
            Debug.Log("FIND FIRST LEAF");
            FindLeaf();
            return;
        }
        BTNode.BTOutcome outcome = currentNode.btState();
        if(outcome == BTNode.BTOutcome.SWITCH){
            Debug.Log("FIND NEW LEAF");
            FindLeaf();
        }
    }



}
