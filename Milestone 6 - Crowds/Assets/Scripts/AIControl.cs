using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum ObstacleType {
    Horror,
    Attractive
}

public class AIControl : MonoBehaviour {
    GameObject[] goalLocations;
    NavMeshAgent agent;
    Animator animator;
    float speedMultiplier;

    float detectionRadius = 250;
    float fleeRadius = 500;

    void ResetAgent() {
        speedMultiplier = Random.Range(0.75f, 1.5f);
        agent.speed = 2 * speedMultiplier;
        agent.angularSpeed = 120;
        animator.SetFloat("speedMultiplier", speedMultiplier);
        animator.SetTrigger("isWalking");
        agent.ResetPath();
    }

    // Start is called before the first frame update
    void Start() {
        goalLocations = GameObject.FindGameObjectsWithTag("goal");
        agent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();

        animator.SetTrigger("isWalking");
        animator.SetFloat("wOffset", Random.Range(0.1f, 1f));

        ResetAgent();
    }

    // Update is called once per frame
    void LateUpdate() {
        if (agent.remainingDistance < 1) {
            ResetAgent();
            agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        }
    }

    public void DetectNewObstacle (Vector3 location, ObstacleType type) {
        if (type == ObstacleType.Horror) {
            if (Vector3.Distance(location, this.transform.position) < detectionRadius) {

                Vector3 fleeDirection = (this.transform.position - location).normalized; 
                Vector3 newGoal = this.transform.position + fleeDirection * fleeRadius;

                NavMeshPath path = new NavMeshPath(); 
                agent.CalculatePath(newGoal, path);

                if (path.status != NavMeshPathStatus.PathInvalid) {
                    agent.SetDestination(path.corners[path.corners.Length - 1]);
                    animator.SetTrigger("isRunning");
                    agent.speed = agent.speed * 2;
                    agent.angularSpeed = 500;
                }
            }
        }
        if (type == ObstacleType.Attractive) {
            if (Vector3.Distance(location, this.transform.position) < detectionRadius) {

                Vector3 newGoal = location;

                NavMeshPath path = new NavMeshPath(); 
                agent.CalculatePath(newGoal, path);

                if (path.status != NavMeshPathStatus.PathInvalid) {
                    agent.SetDestination(path.corners[path.corners.Length - 1]);
                    animator.SetTrigger("isRunning");
                    agent.speed = agent.speed * 2;
                    agent.angularSpeed = 500;
                }
            }
        }
    }
}
