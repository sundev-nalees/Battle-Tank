using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTankModel
{
    private PlayerTankController tankController;

    private TankScriptableObject tankObject;

    public PlayerTankModel(TankScriptableObject tankScriptableObject)
    {
        this.tankObject = tankScriptableObject;

    }

    public TankScriptableObject GetTankObject()
    {
        return tankObject;
    }

    public void SetTankController(PlayerTankController controller)
    {
        tankController = controller;
    }
}
