using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour {

    Animation animation;
    Transform goal;
    float speed = 25.0f;
    float accuracy = 5.0f;
    float rotSpeed = 5.0f;
    public GameObject waypointManager;
    GameObject[] waypoints;
    GameObject currentNode;
    int currentWaypointIndex = 0;
    Graph graph;
    
    // Start is called before the first frame update
    void Start() {
        waypoints = waypointManager.GetComponent<WaypointManager>().waypoints;
        graph = waypointManager.GetComponent<WaypointManager>().graph;
        currentNode = waypoints[0];

        animation = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void LateUpdate() {
        if (graph.getPathLength() == 0 || currentWaypointIndex == graph.getPathLength()) {
            animation.Stop("Tank Moving");
            animation.Play("Tank Idle");
            return;
        }
        else {
            animation.Stop("Tank Idle");
            animation.Play("Tank Moving");
        }
        
        currentNode = graph.getPathPoint(currentWaypointIndex);

        if (Vector3.Distance(graph.getPathPoint(currentWaypointIndex).transform.position, transform.position) < accuracy)
            currentWaypointIndex++;

        if (currentWaypointIndex < graph.getPathLength()) {
            goal = graph.getPathPoint(currentWaypointIndex).transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x, 0, goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(
                this.transform.rotation,
                Quaternion.LookRotation(direction),
                Time.deltaTime * rotSpeed
            );
            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }
 
    public void GoToHelipad() {
        graph.AStar(currentNode, waypoints[9]);
        currentWaypointIndex = 0;
    }

    public void GoToRuins() {
        graph.AStar(currentNode, waypoints[0]);
        currentWaypointIndex = 0;
    }

    public void GoToFactory() {
        graph.AStar(currentNode, waypoints[10]);
        currentWaypointIndex = 0;
    }

    public void GoToTwinMountains() {
        graph.AStar(currentNode, waypoints[2]);
        currentWaypointIndex = 0;
    }

    public void GoToBarracks() {
        graph.AStar(currentNode, waypoints[11]);
        currentWaypointIndex = 0;
    }

    public void GoToCommandCenter() {
        graph.AStar(currentNode, waypoints[3]);
        currentWaypointIndex = 0;
    }

    public void GoToOilRefinery() {
        graph.AStar(currentNode, waypoints[7]);
        currentWaypointIndex = 0;
    }

    public void GoToTankers() {
        graph.AStar(currentNode, waypoints[1]);
        currentWaypointIndex = 0;
    }

    public void GoToRadar() {
        graph.AStar(currentNode, waypoints[13]);
        currentWaypointIndex = 0;
    }

    public void GoToCommandPost() {
        graph.AStar(currentNode, waypoints[14]);
        currentWaypointIndex = 0;
    }

    public void GoToMiddle() {
        graph.AStar(currentNode, waypoints[15]);
        currentWaypointIndex = 0;
    }
}
