using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour {
    public GameObject horrorCylinder;
    public GameObject attractiveCylinder;
    GameObject[] agents;
    // Start is called before the first frame update
    void Start() {
        agents = GameObject.FindGameObjectsWithTag("agent");
    }

    // Update is called once per frame
    void Update() {

        // Left click for the Horror Cylinder
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit)) {
                Instantiate(horrorCylinder, hit.point, horrorCylinder.transform.rotation);
                foreach (GameObject a in agents) {
                    a.GetComponent<AIControl>().DetectNewObstacle(hit.point, ObstacleType.Horror);
                }
            }
        }

        // Right click for the Attractive Cylinder
        if (Input.GetMouseButtonDown(1)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit)) {
                Instantiate(attractiveCylinder, hit.point, attractiveCylinder.transform.rotation);
                foreach (GameObject a in agents) {
                    a.GetComponent<AIControl>().DetectNewObstacle(hit.point, ObstacleType.Attractive);
                }
            }
        }
    }
}
