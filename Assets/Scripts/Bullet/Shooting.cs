using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Rigidbody shell;
    [SerializeField] private Slider aimSlider;
    [SerializeField] private Transform fireTransform;
    [SerializeField] private BulletScriptableObject bulletObject;

    private float chargingSpeed;
    private float fireTimer;
    private float currentLaunchForce;
    bool fired;

    void Start()
    {
        currentLaunchForce = bulletObject.minLaunchForce;

        aimSlider.value = currentLaunchForce;
        fired = false;
        fireTimer = 0;
        chargingSpeed = (bulletObject.maxLaunchForce - bulletObject.minLaunchForce) / bulletObject.maxChargeTime;
    }

    // Update is called once per frame
    void Update()
    {
        aimSlider.value = bulletObject.minLaunchForce;
        FireCheck();
    }

    void FireCheck()
    {
        if (fireTimer < Time.time) 
        {
            if(currentLaunchForce>=bulletObject.maxLaunchForce && !fired)
            {
                currentLaunchForce = bulletObject.maxLaunchForce;
                Fire();

            }
            else if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                fired = false;
                currentLaunchForce = bulletObject.maxLaunchForce;
            }
            else if (Input.GetKey(KeyCode.Mouse0))
            {
                currentLaunchForce += chargingSpeed * Time.deltaTime;
                aimSlider.value = currentLaunchForce;
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0) && !fired)
            {
                Fire();
            }
        }
    }
    void Fire()
    {
        fired = true;
        fireTimer = Time.time + bulletObject.nextFireDelay;
        Rigidbody shellInstance = Instantiate(shell, fireTransform.position, fireTransform.rotation);
        shellInstance.velocity = currentLaunchForce * fireTransform.forward;
        shellInstance.GetComponent<BulletExplosion>().SetComponents(bulletObject, this.gameObject);
        currentLaunchForce = bulletObject.minLaunchForce;
    }
}
