using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankModel
{
    private TankController tankController;

    private float movementSpeed;

    private float turnSpeed;

    public TankModel(float movement,float turn)
    {
        movementSpeed = movement;
        turnSpeed = turn;

    }

    public float GetMovementSpeed()
    {
        return movementSpeed;
    }

    public float GetTurnSpeed()
    {
        return turnSpeed;
    }

    public void SetTankController(TankController controller)
    {
        tankController = controller;
    }
}
