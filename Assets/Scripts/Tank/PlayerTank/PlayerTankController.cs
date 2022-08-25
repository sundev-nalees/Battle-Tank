using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTankController 
{
    private PlayerTankModel tankModel;
    private PlayerTankView tankView;
    private Rigidbody rigidBody;
    private TankScriptableObject tankObject;
    private float currentHealth;
    private bool isDead;



    public PlayerTankController(PlayerTankModel Model)
    {
        tankModel = Model;
        tankModel.SetTankController(this);
        tankObject = tankModel.GetTankObject();
        currentHealth = tankObject.maxHealth;
        isDead = false;
        
    }
    public void Movement(float movementInputValue)
    {
        Vector3 movement = tankView.gameObject.transform.forward * movementInputValue * tankObject.movementSpeed * Time.deltaTime;

        rigidBody.MovePosition(rigidBody.position + movement);
    }
    public void Rotate(float turnInputValue)
    {
        float turn = turnInputValue * tankObject.turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rigidBody.MoveRotation(rigidBody.rotation * turnRotation);
    }
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        tankView.SetHealthUI(currentHealth);
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            tankView.OnDeath();
        }
    }
    public PlayerTankModel GetTankModel()
    {
        return tankModel;
    }

    public PlayerTankView GetTankView()
    {
        return tankView;
    }
    public void SetTankView(PlayerTankView view)
    {
        tankView = view;
        rigidBody = tankView.GetRigidbody();
    }
    
    
 
}
