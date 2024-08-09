using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;

// Require CharacterController
[RequireComponent(typeof(CharacterController))]

public class Player : MonoBehaviour {
    [SerializeField] Camera playerCamera;
    [SerializeField] float speed;
    [SerializeField] float lookSpeed;
    [SerializeField] float gravity;
    float jumpSpeed;
    float lookXLimit = 45f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    void Start() {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        jumpSpeed = gravity + gravity/4;
    }

    void Update() {
        // Get player run input
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float moveSpeed = isRunning ? speed * 3 : speed;

        // Get player look direction
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        float curSpeedX = moveSpeed * Input.GetAxis("Vertical");
        float curSpeedY = moveSpeed * Input.GetAxis("Horizontal");
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        moveDirection.y -= gravity * Time.deltaTime;

        // Jumping
        if (Input.GetKey(KeyCode.Space))
            moveDirection.y = -jumpSpeed;

        // Move player
        characterController.Move(moveDirection * Time.deltaTime);

        // Mouse movement
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }
}
