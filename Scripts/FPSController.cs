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

    [Tooltip("Mouse Sensitivity")]
    public float mouseSensitivity = 3;


    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;

    private Vector2 rotation = Vector2.zero;

    void Start() {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerState.paused) {
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

            rotation.y += Input.GetAxis("Mouse X");
            rotation.x += -Input.GetAxis("Mouse Y");
            rotation.x = Mathf.Clamp(rotation.x, -15f, 15f);
            transform.eulerAngles = new Vector2(0,rotation.y) * mouseSensitivity;
            Camera.main.transform.localRotation = Quaternion.Euler(rotation.x * mouseSensitivity, 0, 0);
        }  
    }
}
