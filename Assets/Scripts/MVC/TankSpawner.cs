using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpawner : MonoBehaviour
{
    public TankView tankView;
    public float movementSpeed;
    public float turnSpeed;
    public Joystick joystick;
    private void Start()
    {
        CreateTank();
    }

    void CreateTank()
    {
        TankModel tankModel = new TankModel(movementSpeed, turnSpeed);
        TankController tankController = new TankController(tankModel, tankView, joystick);
    }
}
