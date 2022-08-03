using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public int playerNumber = 1;
    public float speed = 12f;
    public float turnSpeed = 180f;
    public AudioSource movementAudio;
    public AudioClip engineIdling;
    public AudioClip engineDriving;
    public float pitchRange=0.2f;
    public Joystick joystick;

    private string movementAxisName;
    private string turnAxisName;
    private Rigidbody rigidBody;
    private float movementInputValue;
    private float turnInputValue;
    private float orginalPitch;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        rigidBody.isKinematic = false;
        movementInputValue = 0f;
        turnInputValue = 0f;

    }
    private void OnDisable()
    {
        rigidBody.isKinematic = true;
    }

    private void Start()
    {   
        orginalPitch = movementAudio.pitch;
    }

    private void Update()
    {
        
        movementInputValue = joystick.Vertical;
        turnInputValue = joystick.Horizontal;

        engineAudio();
    }


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

    private void FixedUpdate()
    {
       
        move();
        turn();
    }

    private void move()
    {
        
        Vector3 movement = transform.forward * movementInputValue * speed * Time.deltaTime;
        
        rigidBody.MovePosition(rigidBody.position + movement); 
    }

    private void turn()
    {
        
        float turn = turnInputValue * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rigidBody.MoveRotation(rigidBody.rotation * turnRotation);
    }
    

 
}
