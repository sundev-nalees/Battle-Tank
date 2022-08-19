using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    private Transform tankTransform;



    CinemachineVirtualCamera cvCamera;
    private void Awake()
    {
        cvCamera = GetComponent<CinemachineVirtualCamera>();

    }

    private void Start()
    {
        
        
        tankTransform = GameObject.FindGameObjectWithTag("Player").transform;
        cvCamera.Follow = tankTransform;
        cvCamera.LookAt = tankTransform;
    }
}
