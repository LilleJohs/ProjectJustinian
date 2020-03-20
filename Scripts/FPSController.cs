using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Default speed of character")]
    public float walkSpeed = 5.0f;

    [Tooltip("Run speed multiplier")]
    public float runSpeedMultiplier = 2.0f;

    [Tooltip("Jump speed")]
    public float jumpSpeed = 5.0f;


    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;

    void Start() {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            walkSpeed *= runSpeedMultiplier;
        } else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            walkSpeed /= runSpeedMultiplier;
        }

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 left = transform.TransformDirection(Vector3.left);
        Vector3 planeMovement = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) {
            planeMovement += forward * walkSpeed;
        } else if (Input.GetKey(KeyCode.S)) {
            planeMovement -= forward * walkSpeed;
        }

        if (Input.GetKey(KeyCode.A)) {
            planeMovement += left * walkSpeed;
        } else if (Input.GetKey(KeyCode.D)) {
            planeMovement -= left * walkSpeed;
        }
        if (controller.isGrounded) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                moveDirection.y = jumpSpeed;
            }
        } else {
            moveDirection.y -= 9.81f * Time.deltaTime;
        }
        
        controller.Move((moveDirection + planeMovement) * Time.deltaTime);
    }
}
