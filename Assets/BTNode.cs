using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTNode
{

    public enum BTOutcome{
        CONTINUE,
        SWITCH
    }
    BTOutcome outcome;
    public delegate BTOutcome BTState(); //returns false when we need to change states
    public BTState btState;
    public delegate bool ApproveNode();
    public BTNode parent;
    public List<BTNode> children;
    public List<ApproveNode> approvals;


    public BTNode(){
        parent = null;
        children = new List<BTNode>();
        approvals= new List<ApproveNode>();
    }

    public void AddChild(BTNode childNode, ApproveNode approveNode)
    {
        children.Add(childNode);
        approvals.Add(approveNode);
    }

    public BTNode SelectChild(){
        if(IsLeaf()){
            return this;
        }

        for(int i = 0; i< children.Count; i++){
            if(approvals[i]()){
                return children[i].SelectChild();
            }
        }

        return this; //in the event that everything failed
    }

    public bool IsLeaf(){
        return children.Count == 0;
    }
}
