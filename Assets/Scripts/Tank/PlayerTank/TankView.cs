using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankView : MonoBehaviour
{
    private TankController tankController;
    


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
        movementInputValue = Input.GetAxis("Vertical");
        if (movementInputValue != 0)
        {
            tankController.Movement(movementInputValue);
        }
    }
    private void Turn()
    {
        turnInputValue = Input.GetAxis("Horizontal");
        if (turnInputValue != 0)
        {
            tankController.Rotate(turnInputValue);
        }
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
