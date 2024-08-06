using System.Collections;
using System.Collections.Generic;
//using System.Linq;
using UnityEngine;

public class WaypointFollow : MonoBehaviour {

    //public GameObject[] waypoints;
    public UnityStandardAssets.Utility.WaypointCircuit circuit;
    int currentWaypointIndex = 0;

    [SerializeField] float speed = 32;
    [SerializeField] float rotSpeed = 8;
    [SerializeField] float Accuracy = 1;

    void Start() {
        //waypoints = GameObject.FindGameObjectsWithTag("Waypoint").OrderBy(go => go.name).ToArray();
    }

    void LateUpdate() {
        if(circuit.Waypoints.Length == 0) return;   // Early return if there are no waypoints

        GameObject currentWaypoint = circuit.Waypoints[currentWaypointIndex].gameObject;

        Vector3 lookAtGoal = new Vector3(
            currentWaypoint.transform.position.x,
            this.transform.position.y,
            currentWaypoint.transform.position.z
        );

        Vector3 direction = lookAtGoal - this.transform.position;

        if (direction.magnitude < 1.0f) {
            currentWaypointIndex++;

            if (currentWaypointIndex >= circuit.Waypoints.Length) {
                currentWaypointIndex = 0;
            }
        }

        this.transform.rotation = Quaternion.Slerp(
            this.transform.rotation,
            Quaternion.LookRotation(direction),
            Time.deltaTime * rotSpeed
        );

        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}