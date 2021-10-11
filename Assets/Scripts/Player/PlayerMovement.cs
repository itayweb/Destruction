using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Player playerScript;
    [SerializeField] float accelerationSpeed;
    [SerializeField] float reverseSpeed;
    [SerializeField] float turnSpeed;

    private InputManager inputManager;
    private InputAction steering;
    private InputAction acceleration;
    private Rigidbody sphereRb;
    private float playerInput;
    private float turnInput;

    private void Awake()
    {
        sphereRb = GetComponentInChildren<Rigidbody>();
        playerScript = GetComponent<Player>();
        inputManager = new InputManager();
        sphereRb.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        CarToSphere();
        GetPlayerInput();
        RotateCar();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        sphereRb.AddForce(transform.forward * playerInput, ForceMode.Acceleration);
    }

    private void RotateCar()
    {
        float newRotation = turnInput * turnSpeed * Time.deltaTime * acceleration.ReadValue<float>();
        transform.Rotate(0, newRotation, 0, Space.World);
    }

    void CarToSphere() // Set the player car position to the motor sphere postion
    {
        transform.position = sphereRb.transform.position;
    }

    void GetPlayerInput()
    {
        playerInput = acceleration.ReadValue<float>();
        turnInput = steering.ReadValue<float>();
        if (playerInput > 0)
        {
            playerInput *= accelerationSpeed * -1;
        }
        else
        {
            playerInput *= reverseSpeed * -1;
        }
    }

    private void OnEnable()
    {
        steering = inputManager.Player.Steering;
        acceleration = inputManager.Player.Acceleration;
        inputManager.Player.Enable();
    }

    private void OnDisable()
    {
        inputManager.Player.Disable();
    }
}
