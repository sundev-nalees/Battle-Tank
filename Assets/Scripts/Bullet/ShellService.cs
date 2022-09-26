using System;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class ShellService : MonoSingletonGeneric<ShellService>
    {
        [SerializeField] ShellView shellPrefab;
        private BulletServicePool shellServicePool;

        private void OnEnable()
        {
            shellServicePool = GetComponent<BulletServicePool>();
        }

        public void GetShell(BulletScriptableObject bulletObject,Transform spawnPoint,Vector3 velocity)
        {
            ShellController shellController = shellServicePool.GetBullet(shellPrefab, bulletObject);
            if (shellController != null)
            {
                ShellView shellView = shellController.GetShellView;
                PlayFireSound();
                shellView.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
                shellView.GetComponent<Rigidbody>().velocity = velocity;

            }
            
        }

        private void PlayFireSound()
        {
            var instance = AudioManager.Instance;
            if (instance)
            {
                instance.PlaySound(SoundType.Fire);
            }
        }
        public void ReturnToPool(ShellController shellController)
        {
            shellServicePool.ReturnItem(shellController);
        }
    }
}