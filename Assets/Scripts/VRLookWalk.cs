using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRLookWalk : MonoBehaviour
{
    public float movementSpeed = 3.0f;
    public float rotationSpeed = 2.0f;
    public float gravity = 9.81f; // Adjust this value to control the strength of gravity

    private CharacterController characterController;
    private Transform vrCamera;
    private Vector3 velocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Find the VR camera in the scene (e.g., Oculus or SteamVR camera)
        vrCamera = Camera.main.transform;
    }

    void Update()
    {
        // Character Movement
        float moveDirectionX = Input.GetAxis("Horizontal");
        float moveDirectionZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveDirectionX, 0, moveDirectionZ);
        movement = vrCamera.TransformDirection(movement);
        movement *= movementSpeed * Time.deltaTime;

        // Apply gravity
        if (!characterController.isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = -0.5f; // Reset vertical velocity when grounded to prevent bouncing
        }

        // Move the character
        characterController.Move(movement + velocity * Time.deltaTime);

        // Camera Rotation (using joystick)
        float joystickX = Input.GetAxis("JoystickX"); // Adjust the axis name based on your joystick input
        float joystickY = Input.GetAxis("JoystickY"); // Adjust the axis name based on your joystick input

        Vector3 rotation = new Vector3(joystickY, joystickX, 0) * rotationSpeed * Time.deltaTime;
        vrCamera.localRotation *= Quaternion.Euler(rotation);
    }
}
