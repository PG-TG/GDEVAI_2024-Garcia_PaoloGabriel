using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour {
    [SerializeField] GameObject player;
    GameObject[] agents;
    // Start is called before the first frame update
    void Start() {
        agents = GameObject.FindGameObjectsWithTag("AI");
    }

    // Update is called once per frame
    void Update() {
        foreach (GameObject ai in agents) {
            ai.GetComponent<AIControl>().agent.SetDestination(player.transform.position);
        }
    }
}
