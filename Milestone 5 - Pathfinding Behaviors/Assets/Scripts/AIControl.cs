using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

public enum Personality {
    Aggresive,
    Evasive,
    Cowardly
}

public class AIControl : MonoBehaviour {
NavMeshAgent agent;
public GameObject target;
public float engageDistance = 10;
public Personality personality;
public WASDMovement playerMovement;

    // Start is called before the first frame update
    void Start() {
        agent = this.GetComponent<NavMeshAgent>();
        playerMovement = target.GetComponent<WASDMovement>();
    }
    void Seek(Vector3 location) {
            agent.SetDestination(location);
    }
    void Flee(Vector3 location) {
        Vector3 fleeDirection = location - this.transform.position;
        agent.SetDestination(this.transform.position - fleeDirection);
    }
    void Pursue() {
        Vector3 targetDirection = target.transform.position - this.transform.position;
        float lookAhead = targetDirection.magnitude/(agent.speed + playerMovement.currentSpeed);
        Seek(target.transform.position + target.transform.forward* lookAhead);
    }
    void Evade() {
        Vector3 targetDirection = target.transform.position - this.transform.position;
        float lookAhead = targetDirection.magnitude/(agent.speed + playerMovement.currentSpeed);
        Flee(target.transform.position + target.transform.forward * lookAhead);
    }
    Vector3 wanderTarget;
    void Wander() {
        float wanderRadius = 20;
        float wanderDistance = 10;
        float wanderJitter = 1;

        wanderTarget += new Vector3(
            Random.Range(-1.0f, 1.0f) * wanderJitter,
            0,
            Random.Range(-1.0f, 1.0f) * wanderJitter
        );
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);

        Seek(targetWorld);
    } 
    void Hide() {
        float distance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;

        int hidingSpotsCount = World.Instance.GetHidingSpots().Length;

        for (int i = 0; i < hidingSpotsCount; i++) {
            Vector3 hideDirection = World.Instance.GetHidingSpots()[i].transform.position - target.transform.position;
            Vector3 hidePosition = World.Instance.GetHidingSpots ()[i].transform.position + hideDirection.normalized * 5; //distance offset

            float spotDistance = Vector3.Distance(this.transform.position, hidePosition);
            if (spotDistance < distance) {
                chosenSpot = hidePosition;
                distance = spotDistance;
            }
        }

        Seek(chosenSpot);
    }
    void CleverHide() {
        float distance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        Vector3 chosenDirection = Vector3.zero;
        GameObject chosenGameObject = World.Instance.GetHidingSpots()[0];

        int hidingSpotsCount = World.Instance.GetHidingSpots().Length;

        for (int i = 0; i < hidingSpotsCount; i++) {
            Vector3 hideDirection = World.Instance.GetHidingSpots()[i].transform.position - target.transform.position;
            Vector3 hidePosition = World.Instance.GetHidingSpots ()[i].transform.position + hideDirection.normalized * 5; //distance offset

            float spotDistance = Vector3.Distance(this.transform.position, hidePosition);
            if (spotDistance < distance) {
                chosenSpot = hidePosition;
                chosenDirection = hideDirection;
                chosenGameObject = World.Instance.GetHidingSpots()[i];
                distance = spotDistance;
            }
        }

        Collider hideCollider = chosenGameObject.GetComponent<Collider>();
        Ray back = new Ray(chosenSpot, -chosenDirection.normalized);
        RaycastHit info;
        float rayDistance = 100.0f;
        hideCollider.Raycast(back,  out info, rayDistance);

        Seek(info.point + chosenDirection.normalized * 5);
    }

    // Update is called once per frame
    void Update() {
        float targetDistance = Vector3.Distance(this.transform.position, target.transform.position);
        
        /*
        [ ZOMBIE PERSONALITIES ]
        RED   - Aggressive
        BLUE  - Evasive
        GREEN - Cowardly
        */
        switch (personality) {
            case Personality.Aggresive:
                if (targetDistance <= engageDistance)   Pursue();
                else                                    Wander();
                break;

            case Personality.Evasive:
                if (targetDistance <= engageDistance)   Evade();
                else                                    Wander();
                break;

            case Personality.Cowardly:
                if (targetDistance <= engageDistance)   CleverHide();
                else                                    Wander();
                break;
        }
    }
}