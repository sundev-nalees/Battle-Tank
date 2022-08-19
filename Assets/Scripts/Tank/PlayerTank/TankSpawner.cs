using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpawner : MonoBehaviour
{
    [SerializeField] TankView tankView;
    [SerializeField] float movementSpeed;
    [SerializeField] float turnSpeed;
    
    private void OnEnable()
    {
        CreateTank();
    }

    void CreateTank()
    {
        TankModel tankModel = new TankModel(movementSpeed, turnSpeed);
        TankController tankController = new TankController(tankModel, tankView);
    }
}
