using System;
using UnityEngine;
using UnityEngine.UI;

namespace TankGame
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] private BulletExplosion bulletPrefab;
        [SerializeField] private Slider aimSlider;
        [SerializeField] private Transform fireTransform;
        [SerializeField] private BulletScriptableObject bulletObject;

        private BulletServicePool bulletServicePool;
        private float chargingSpeed;
        private float fireTimer;
        private float currentLaunchForce;
        bool fired;
        private int bulletCount;

        public static event Action<int> OnBulletFired;

        void Start()
        {
            currentLaunchForce = bulletObject.minLaunchForce;
            bulletServicePool = GetComponent<BulletServicePool>();
            aimSlider.value = currentLaunchForce;
            fired = false;
            fireTimer = 0;
            chargingSpeed = (bulletObject.maxLaunchForce - bulletObject.minLaunchForce) / bulletObject.maxChargeTime;
        }


        void Update()
        {
            aimSlider.value = bulletObject.minLaunchForce;
            FireCheck();
        }

        void FireCheck()
        {
            if (fireTimer < Time.time)
            {
                if (currentLaunchForce >= bulletObject.maxLaunchForce && !fired)
                {
                    currentLaunchForce = bulletObject.maxLaunchForce;
                    Fire();

                }
                else if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    fired = false;
                    currentLaunchForce = bulletObject.minLaunchForce;
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
            bulletCount++;
            CheckAchievement();
            fireTimer = Time.time + bulletObject.nextFireDelay;
            ConfigureBullet();
        }

        private void ConfigureBullet()
        {
            if (bulletServicePool == null)
            {
                Debug.Log("blaaa");
            }
            BulletExplosion bulletInstance = bulletServicePool.GetBullet(bulletPrefab, fireTransform);
            bulletInstance.GetComponent<Rigidbody>().velocity = currentLaunchForce * fireTransform.forward;
            bulletInstance.GetComponent<BulletExplosion>().SetComponents(bulletObject, this.gameObject, bulletServicePool);
            currentLaunchForce = bulletObject.minLaunchForce;
        }

        private void CheckAchievement()
        {
            if (bulletCount == 10 || bulletCount == 20 || bulletCount == 30)
            {
                OnBulletFired?.Invoke(bulletCount);
            }
        }
    }
}