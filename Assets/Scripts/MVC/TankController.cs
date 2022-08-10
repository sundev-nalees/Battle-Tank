using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController 
{
    TankModel tankModel;

    TankView tankView;

    Rigidbody rigidBody;


    float movementSpeed;
    float turnSpeed;



    public TankController(TankModel Model,TankView View,Joystick joystick)
    {
        tankModel = Model;
        tankView = GameObject.Instantiate<TankView>(View);
        tankView.SetJoystick(joystick);
        rigidBody = tankView.GetRigidbody();
        tankView.SetTankController(this);
        tankModel.SetTankController(this);

        GetData();

    }
    public void Movement(float movementInputValue)
    {
        Vector3 movement = tankView.gameObject.transform.forward * movementInputValue * movementSpeed * Time.deltaTime;

        rigidBody.MovePosition(rigidBody.position + movement);
    }
    public void Rotate(float turnInputValue)
    {
        float turn = turnInputValue * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rigidBody.MoveRotation(rigidBody.rotation * turnRotation);
    }

    public TankModel GetTankModel()
    {
        return tankModel;
    }

    public TankView GetTankView()
    {
        return tankView;
    }
    public void SetTankView(TankView view)
    {
        tankView = view;
        rigidBody = tankView.GetRigidbody();
    }
    void GetData()
    {
        movementSpeed = tankModel.GetMovementSpeed();
        turnSpeed = tankModel.GetTurnSpeed();
    }
    /*


    private void engineAudio() 
    {
        
        if (Mathf.Abs(movementInputValue) < 0.1f && Mathf.Abs(turnInputValue) < 0.1f)
        {
            if (movementAudio.clip == engineDriving)
            {
                movementAudio.clip = engineIdling;
                movementAudio.pitch = Random.Range(orginalPitch - pitchRange, orginalPitch + pitchRange);
                movementAudio.Play();
            }
        }
        else
        {
            if (movementAudio.clip == engineIdling)
            {
                movementAudio.clip = engineDriving;
                movementAudio.pitch = Random.Range(orginalPitch - pitchRange, orginalPitch + pitchRange);
                movementAudio.Play();
            }
        }
    }

    

    
    
    */
 
}
