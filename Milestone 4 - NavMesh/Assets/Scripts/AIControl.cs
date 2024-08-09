using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControl : MonoBehaviour {
    public UnityEngine.AI.NavMeshAgent agent;

    Animation anim; 
    float speed;
    // Start is called before the first frame update
    void Start() {
        this.agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        this.anim = this.GetComponent<Animation>();
        anim.Play("Agent Idle");
    }
    /*
    void FixedUpdate() {

        Vector3 minVelocity = new Vector3(agent.speed/5, agent.speed/5, agent.speed/5);
        if (agent.velocity.magnitude >= minVelocity.magnitude) {
            anim.Play("Agent Walk");
        }
        else {
            anim.Play("Agent Idle");
        }
    }
    */ 
}
