using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class pet : MonoBehaviour {
    [SerializeField] Transform target;
    [SerializeField] float maxSpeed;

    void LateUpdate() {
        // Dynamically update move and rotation speed with distance
        float distance = Vector3.Distance(target.position, this.transform.position);
        float moveSpeed = Mathf.Clamp(distance, 1, maxSpeed);
        float rotSpeed = Mathf.Clamp(maxSpeed - moveSpeed, maxSpeed/4, maxSpeed);
        
        // Set direction and rotation
        Vector3 lookAtTarget = new Vector3(target.position.x, this.transform.position.y, target.position.z);
        Vector3 direction = lookAtTarget - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);
        
        // Stop moving towards player if too close
        if (Vector3.Distance(lookAtTarget, transform.position) > 3) {
            transform.Translate(0, 0, moveSpeed * Time.deltaTime);
        }
    }
}
