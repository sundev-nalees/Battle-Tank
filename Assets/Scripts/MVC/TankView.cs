using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankView : MonoBehaviour
{
    public TankController tankController;
    Joystick joystick;


    private float movementInputValue;
    private float turnInputValue;
    
    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        tankController.SetTankView(this);
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        movementInputValue = joystick.Vertical;
        if (movementInputValue != 0)
        {
            tankController.Movement(movementInputValue);
        }
    }
    private void Turn()
    {
        turnInputValue = joystick.Horizontal;
        if (turnInputValue != 0)
        {
            tankController.Rotate(turnInputValue);
        }
    } 

    public void SetJoystick(Joystick _joystick)
    {
        joystick = _joystick;
    }

    public Rigidbody GetRigidbody()
    {
        return rigidBody;
    }

    public void SetTankController(TankController _controller)
    {
        tankController = _controller;
    }
}
