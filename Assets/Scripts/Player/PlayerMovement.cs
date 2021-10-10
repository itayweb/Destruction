using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Player playerScript;
    [SerializeField] float moveSpeed;

    private InputManager inputManager;
    private InputAction steering;
    private InputAction acceleration;
    private Rigidbody sphereRb;
    private float playerInput;

    private void Awake()
    {
        sphereRb = GetComponentInChildren<Rigidbody>();
        playerScript = GetComponent<Player>();
        inputManager = new InputManager();
    }

    // Update is called once per frame
    void Update()
    {
        CarToSphere();
        GetPlayerInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        sphereRb.AddForce(transform.forward * playerInput, ForceMode.Acceleration);
    }

    void CarToSphere() // Set the player car position to the motor sphere postion
    {
        transform.position = sphereRb.transform.position;
    }

    void GetPlayerInput()
    {
        playerInput = acceleration.ReadValue<Vector2>().x;
        playerInput *= moveSpeed;
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
