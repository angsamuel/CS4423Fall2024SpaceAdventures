using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{

    //layers of things we want to find
    [Header("Config")]
    public LayerMask pathBlockMask;
    public float aStarScanDist = 1;
    public bool spawnMarkers = true;

    [Header("Trackers")]
    public bool findingPath = false;
    public bool reachedDestination = false;


    [Header("Prefabs")]
    public GameObject waypointMarker;

    [Header("Objects")]
    public Transform goal;
    Creature myCreature;
    // Start is called before the first frame update
    void Start()
    {
        myCreature = GetComponent<Creature>();
        StartCoroutine(Pathfind());
    }



    IEnumerator Pathfind(){
        bool needPath = true;
        while(!reachedDestination){

            //generate the path
            GetAStarToTarget(goal.transform.position);
            yield return new WaitUntil(()=>findingPath == false);
            needPath = false;

            //visualize the path
            foreach(Vector2 v in aStarPath){
                Instantiate(waypointMarker,v,Quaternion.identity);
                yield return new WaitForSeconds(.1f);
            }

            //move creature along path
            foreach(Vector2 v in aStarPath){
                while(Vector3.Distance(transform.position,v) > myCreature.speed * Time.fixedDeltaTime){

                    //see if we can reach the next node in the sequence
                    if(CanReachPos(transform.position,v)){
                        myCreature.MoveToward(v);
                    }else{ //otherwise, we need to generate a new path, could be optimized
                        needPath = true;
                        break;
                    }
                    yield return new WaitForFixedUpdate();
                }
                if(needPath){
                    break;
                }else{
                    transform.position = v; //ensure we're hitting the grid exactly
                }
            }

            //we made it yay!
            if(!needPath){
                reachedDestination = true;
            }

            yield return null;
        }

        myCreature.Stop();
    }



    void GetAStarToTarget(Vector3 targetPos){
        findingPath = true;
        aStarPath = new List<Vector2>();
        StartCoroutine(AStarToTargetRoutine(targetPos));
    }

    IEnumerator AStarToTargetRoutine(Vector3 targetPos){

        Vector3 myPos = transform.position;

        HashSet<Vector2> closedSet = new HashSet<Vector2>(); //blocks already evaluated

        HashSet<Vector2> openSet = new HashSet<Vector2>(); //currently discovered nodes that are not evaluated yet
        openSet.Add(myPos);

        cameFrom = new Dictionary<Vector2, Vector2>();

        //gscore is the cost of getting to a position
        Dictionary <Vector2, float> gScore = new Dictionary<Vector2, float>();
        gScore.Add(myPos, 0);

        //fscore is the gscore, plus the heuristic
        Dictionary <Vector2, float> fScore = new Dictionary<Vector2, float>();
        fScore.Add(myPos, HeuristicCostEstimate(myPos,targetPos));

        int maxAttempts = 400; //prevent us from looking forever if path gets too large

        while (openSet.Count > 0 && maxAttempts > 0){
            maxAttempts -= 1;

            //find the vector in open set with smallest f, set it as our current
            float minF = int.MaxValue;
            Vector2 minFVector = new Vector2(-1,-1);

            //lazy! use a priority queue dum dum
            foreach(Vector2 v in openSet){
                if(fScore[v] < minF){
                    minF = fScore[v];
                    minFVector = v;
                }
            }

            Vector2 current = minFVector;

            yield return null; //time slice


            if(Vector2.Distance(current,targetPos) <= aStarScanDist && CanReachPos(current,targetPos)){ //we've made it to our target
                ReconstructPath(current);
            }else{

                openSet.Remove(current); //remove our current space from the evaluation pool
                closedSet.Add(current); //add to the closed pool


                //neighbors are the spaces in our imaginary grid to the north, south, east, and west
                List<Vector2> neighbors = new List<Vector2>();
                for(float x = -1; x < 2; x++){
                    for(float y = -1; y < 2; y++){
                        if(x == 0 || y == 0){
                            if(CanReachPos(current, current + new Vector2(x*aStarScanDist, y*aStarScanDist))){
                                neighbors.Add(current + new Vector2(x,y)*aStarScanDist);
                            }
                        }

                    }
                }


                foreach(Vector2 neighbor in neighbors){
                    if (!closedSet.Contains(neighbor)){
                        float new_gScore = gScore[current] + aStarScanDist;

                        bool worsePath = false;
                        if(!openSet.Contains(neighbor)){
                            openSet.Add(neighbor);
                        }else if(new_gScore >= gScore[neighbor]){
                            worsePath = true;
                        }

                        if(!worsePath){
                            cameFrom[neighbor] = current;
                            gScore[neighbor] = new_gScore;
                            fScore[neighbor] = gScore[neighbor] + HeuristicCostEstimate(neighbor, targetPos);
                        }
                    }
                }
            }

        }

        findingPath = false;
    }

    bool CanReachPos(Vector3 startPos, Vector3 endPos){
        return !Physics2D.Linecast(startPos,endPos,pathBlockMask);
    }

    //estimates distance between position on grid, and destination
    float HeuristicCostEstimate(Vector2 start, Vector2 goal){
        return Mathf.Abs(start.x - goal.x) + Mathf.Abs(start.y - goal.y);
    }
    public List<Vector2> aStarPath;
    Dictionary<Vector2, Vector2> cameFrom;
    void ReconstructPath(Vector2 current){

        //wipe our path vector
        aStarPath = new List<Vector2>();

        //add our path
        aStarPath.Add(current);
        while(cameFrom.ContainsKey(current)){
            current = cameFrom[current];
            aStarPath.Add(current);
        }

        //reverse list to make things simple
        aStarPath.Reverse();

        //don't forget to add final goal position!
        aStarPath.Add(goal.transform.position);
    }
}